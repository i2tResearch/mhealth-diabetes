using CAC.Interfaces;
using CAC.Library.BP;
using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using CAC.Library.Model.Transform;
using CAC.Library.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CAC.Services
{
    public class FileService : IFileService
    {
       
        private SingletonDB controllerDB = SingletonDB.getInstance();

        //representa la conexión a la base datos inicializando un objeto contexto de Entity Framework
        private ModelContainer db = null;

        public FileService()
        {
            db = controllerDB.DB;
        }

        public List<DTOArchivo> ListFileByUser(string id)
        {
            List<DTOArchivo> response = new List<DTOArchivo>();

            try
            {
                Archivo_Factory archivoFactory = new Archivo_Factory();
                Guid idUsuario = Guid.Parse(id);
                List<tbl_archivo_cac> lstArchivosCAC = db.tbl_archivo_cac.Where(arch => arch.idUsuario == idUsuario).ToList();
                response = archivoFactory.transformListDTO(lstArchivosCAC);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<FileService>());
            }
            return response.OrderByDescending(m => m.FechaCreacion).ToList();
        }

        public bool ValidatePriorityPatient(Stream log)
        {           

            byte[] binario;
            try
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = log.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    binario = ms.ToArray();
                    // llAMAR ESTE METODO
                   // priorityPatient.Build("nombre archivo.xls", binario, )
                    
                }
            }
            catch (Exception ex)
            {
                 IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<FileService>());
                return false;
            }

            return true;
        }

        private async void FirebaseStorageRun(byte[] binario, DTOArchivo dto)
        {
            try
            {
                int total = -1;
                if (dto != null)
                {
                    var re = await FirebaseController.WriteOnFirebaseStorage(binario, dto);
                    dto.UrlArchivo = re;
                    Archivo_Factory factory = new Archivo_Factory();
                    tbl_archivo_cac modelo = factory.transformModel(dto);

                    var temp_modelo = db.tbl_archivo_cac.Where(m => m.id == modelo.id).FirstOrDefault();

                    if(temp_modelo != null)
                    {
                        temp_modelo.urlArchivo = re;
                        db.tbl_archivo_cac.Attach(temp_modelo);
                        db.Entry(temp_modelo).State = System.Data.EntityState.Modified;
                        total = db.SaveChanges();
                    }
                    //IOUtilities.WriteLog(string.Format("{0}\t{1}\tFirebaseStorageRun\t{2}", IOUtilities.GetLocalTime(), Configuration.GetClassName<FileService>(), total), Configuration.GetClassName<IOUtilities>(), Configuration.GetValueConf(Constants.LogFile));
                }
            }
            catch (Exception ex)
            {
                 IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<FileService>());
            }
        }

        private int MySQLStorageRun(DTOArchivo archivo)
        {
            int total = -1;
            try
            {
                Archivo_Factory factory = new Archivo_Factory();
                tbl_archivo_cac modelo = factory.transformModel(archivo);
                var temp_modelo = db.tbl_archivo_cac.Where(m => m.id == modelo.id).FirstOrDefault();
                if (temp_modelo == null)
                {
                    db.tbl_archivo_cac.Add(modelo);
                    total = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                 IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<FileService>());
            }
            return total;
        }

        public DTOResponse Validator(DTOTransporteArchivo log)
        {
            DTOResponse response = new DTOResponse();
            try
            {
                
                if (log.Archivo != null && log.Binario != null && log.Binario.Length > 0)
                {
                    var decompress2 = ZipFile.DecompressGZipStream(log.Binario);
                    var decompress = log.Archivo.Nombre.Contains(".zip") == true ? ZipFile.DecompressZipStream(decompress2) : decompress2;
                    //var decompress = File.ReadAllBytes(@"D:\Users\Candy\Documents\Debugger SND.CAC\Pruebas_mayo\Comprimido\EMPTY_FIELDS2.csv");
                    IFileProcessBP filebp = new FileProcessBP();
                    List<DTOValidacionArchivo> lista = filebp.ValidateFile(log.Archivo.Nombre, decompress);
                    if (lista.Count == 0)
                    {
                        log.Archivo.NumFilasImportadas = filebp.GetTotalRow();
                        log.Archivo.Tamano = decompress.Length.ToString();
                        log.Archivo.UrlArchivo = "NO_URL";
                        int totalsaved = MySQLStorageRun(log.Archivo);
                        if (totalsaved > 0)
                        {
                            FirebaseStorageRun(decompress, log.Archivo);
                            response.Archivo = log.Archivo;
                            System.Threading.Tasks.Task.Factory.StartNew(() => PatientPriority(decompress, log.Archivo));
                        }
                        else
                        {
                            response.Archivo = null;
                            response.List = new List<DTOValidacionArchivo>();
                            DTOValidacionArchivo validation = new DTOValidacionArchivo()
                            {
                                Valor = totalsaved.ToString(),
                                FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                Descripcion = "No se pudo agregar el archivo a la base de datos. Por favor comunicarse con el servicio tecnico."
                            };
                            response.List.Add(validation);
                        }
                    }
                    if (lista.Count > 0)
                    {
                        response.List = lista;
                    }
                }
                else
                {
                    response.Archivo = null;
                    response.List = new List<DTOValidacionArchivo>();
                    DTOValidacionArchivo validation = new DTOValidacionArchivo()
                    {
                        Valor = "",
                        FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                        Descripcion = "No fue adjuntado ningun archivo para realizar el análisis"
                    };
                    response.List.Add(validation);
                }
                return response;
            }
            catch (Exception ex)
            {
                 IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<FileService>());
            }
            return null;
        }

        public DTOResponse Validator2(Stream log)
        {

            DTOResponse response = new DTOResponse();

            byte[] binario;
            try
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = log.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    binario = ms.ToArray();
                    if (binario.Length > 0)
                    {
                        IFileProcessBP filebp = new FileProcessBP();
                        List<DTOValidacionArchivo> lista = filebp.ValidateFile("nombre", binario);
                        if (lista.Count == 0)
                        {
                            response.Archivo = new DTOArchivo() { Nombre = "file.xls", Id = Guid.NewGuid().ToString(), IdUsuario = "f9587aba-0990-11e7-93ae-92361f002671", FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NumFilasImportadas = filebp.GetTotalRow(), Tamano = binario.Length.ToString(), UrlArchivo = "" };
                            int totalsaved = MySQLStorageRun(response.Archivo);
                            if (totalsaved > 0)
                            {
                                FirebaseStorageRun(binario, response.Archivo);

                                PatientPriority(binario, response.Archivo);
                               
                            }
                            else
                            {
                                response.Archivo = null;
                                response.List = new List<DTOValidacionArchivo>();
                                DTOValidacionArchivo validation = new DTOValidacionArchivo()
                                {
                                    Valor = "",
                                    FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                                    Descripcion = "No se pudo agregar el archivo a la base de datos. Por favor comunicarse con el servicio tecnico."
                                };
                                response.List.Add(validation);
                            }

                        }
                        if (lista.Count > 0)
                        {
                            response.List = lista;
                        }
                    }
                    else
                    {
                        response.Archivo = null;
                        response.List = new List<DTOValidacionArchivo>();
                        DTOValidacionArchivo validation = new DTOValidacionArchivo()
                        {
                            Valor = "",
                            FechaCreacion = DateTime.Now.ToString(Configuration.GetValueConf(Constants.DateFormat)),
                            Descripcion = "No fue adjuntado ningun archivo para realizar el análisis"
                        };
                        response.List.Add(validation);
                    }
                    return response;

                }
            }
            catch (Exception ex)
            {
                 IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<FileService>());
            }
            return null;
        }

        private void PatientPriority(byte[] decompress, DTOArchivo dtoArchivo)
        {
            IPriorityPatient priorityPatient = new PriorityPatient();
            priorityPatient.Build(decompress, dtoArchivo.Id, dtoArchivo.Nombre);
            ICACBP cacBP = new CACBP();
            cacBP.saveData(decompress, dtoArchivo.Id, dtoArchivo.Nombre, dtoArchivo.IdUsuario);
        }
    }
}
