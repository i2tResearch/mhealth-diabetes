using CAC.Library.Model.DB;
using CAC.Library.Utilities;
using ClosedXML.Excel;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace CAC.Library.BP
{
    /// <summary>
    /// Inserta en una tabla cada fila del archivo CAC
    /// Autor: Juan Guillermo Gómez
    /// </summary>
    public class CACBP : ICACBP
    {
       

        private string idArchivo;
        private Guid idOrganizacion;
        private string idUsuario;
        private string nameFile;
        private byte[] binaryFile;
        private IXLTableRows rows;
        
        public void saveData(byte[] binaryFile, string idArchivo, string nameFile, string idUsuario)
        {
            this.binaryFile = binaryFile;
            this.idArchivo = idArchivo;
            this.idUsuario = idUsuario;
            this.nameFile = nameFile;
            initFileWorkSheet();
            Task.Factory.StartNew(() => insertCAC());
        }

        /// <summary>
        /// Carga el archivo binario de excel a la libreria para recorrerlo
        /// </summary>
        private void initFileWorkSheet()
        {
            try
            {

                if (nameFile.Contains(".csv") == true)
                {
                    IConvertFormat convert = new ConvertFormat();
                    var workBookCAC = convert.Convert_CSV_ClosedXML("page1", binaryFile, ',');
                    var worksheetCAC = workBookCAC.Worksheet(1);
                    var range = worksheetCAC.Range(worksheetCAC.FirstCellUsed(), worksheetCAC.LastCellUsed());
                    range.Clear(XLClearOptions.Formats);
                    var table = range.AsTable();
                    table.DeleteComments();
                    rows = table.DataRange.Rows();
                }
                else if (nameFile.Contains(".xls") == true || nameFile.Contains(".xlsx"))
                {
                    using (Stream streamFile = new MemoryStream(binaryFile))
                    {
                        using (var workBookCAC = new XLWorkbook(streamFile))
                        {
                            var worksheetCAC = workBookCAC.Worksheet(1);
                            var range = worksheetCAC.Range(worksheetCAC.FirstCellUsed(), worksheetCAC.LastCellUsed());
                            range.Clear(XLClearOptions.Formats);
                            var table = range.AsTable();
                            table.DeleteComments();
                            rows = table.DataRange.Rows();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatient>());
                throw ex;
            }
        }

        /// <summary>
        /// Recorre cada fila del archivo para insertarlos en una tabla
        /// </summary>
        private void insertCAC()
        {
            var db = SingletonDB.getInstance().DB;
            try
            {
                tbl_cac cacRecord;
                var iduser = Guid.Parse(idUsuario);
                var usuario = db.tbl_usuario.Where(usu => usu.id == iduser).SingleOrDefault();

                idOrganizacion = usuario.idOrganizacion;
                int i = 0;
                foreach (var row in rows)
                {
                    cacRecord = createRecord(row);
                    db.tbl_cac.Add(cacRecord);
                    i++;
                    if (i % 1000 == 0)
                    {
                        int j = db.SaveChanges();
                    }
                }

                int totl = db.SaveChanges();
            }

            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<CACBP>());
            }
        }

        /// <summary>
        /// Crea cada registro a insertar en la tabla
        /// </summary>
        /// <param name="row">Fila de datos a insertar</param>
        /// <returns>Registro CAC</returns>
        private tbl_cac createRecord(IXLTableRow row)
        {
            tbl_cac cacRecord = new tbl_cac();

            cacRecord.id = Guid.NewGuid();
            cacRecord.idArchivo = Guid.Parse(idArchivo);
            cacRecord.primerNombre = row.Field("Primer_Nombre").GetString().Trim(' ').Length > 200 ? row.Field("Primer_Nombre").GetString().Trim(' ').Substring(0, 200) : row.Field("Primer_Nombre").GetString().Trim(' ');
            cacRecord.segundoNombre = row.Field("Segundo_Nombre").GetString().Trim(' ').Length > 200 ? row.Field("Segundo_Nombre").GetString().Trim(' ').Substring(0, 200) : row.Field("Segundo_Nombre").GetString().Trim(' ');
            cacRecord.primerApellido = row.Field("Primer_Apellido").GetString().Trim(' ').Length > 200 ? row.Field("Primer_Apellido").GetString().Trim(' ').Substring(0, 200) : row.Field("Primer_Apellido").GetString().Trim(' ');
            cacRecord.segundoApellido = row.Field("Segundo_Apellido").GetString().Trim(' ').Length > 200 ? row.Field("Segundo_Apellido").GetString().Trim(' ').Substring(0, 200) : row.Field("Segundo_Apellido").GetString().Trim(' ');

            cacRecord.tipoIdentificacion = row.Field("Tipo_Identificacion").GetString().Trim(' ').Length > 10 ? row.Field("Tipo_Identificacion").GetString().Trim(' ').Substring(0, 5) : row.Field("Tipo_Identificacion").GetString().Trim(' ');
            cacRecord.numIdentificacion = row.Field("Numero_Identificacion").GetString().Trim(' ').Length > 10 ? row.Field("Numero_Identificacion").GetString().Trim(' ').Substring(0, 5) : row.Field("Numero_Identificacion").GetString().Trim(' ');

            cacRecord.sexo = row.Field("Sexo").GetString().Trim(' ').Length > 2 ? row.Field("Sexo").GetString().Trim(' ').Substring(0, 2) : row.Field("Sexo").GetString().Trim(' ');

            cacRecord.diagHiperArterial = row.Field("Diagnostico_Hipertension_Arterial").GetString().Trim(' ').Length > 5 ? row.Field("Diagnostico_Hipertension_Arterial").GetString().Trim(' ').Substring(0, 5) : row.Field("Diagnostico_Hipertension_Arterial").GetString().Trim(' ');
            cacRecord.diagDiabetesMellitus = row.Field("Diagnostico_Diabetes_Mellitus").GetString().Trim(' ').Length > 5 ? row.Field("Diagnostico_Diabetes_Mellitus").GetString().Trim(' ').Substring(0, 5) : row.Field("Diagnostico_Diabetes_Mellitus").GetString().Trim(' ');
            cacRecord.etiologiaCAC = row.Field("Etiologia_CAC").GetString().Trim(' ').Length > 5 ? row.Field("Etiologia_CAC").GetString().Trim(' ').Substring(0, 5) : row.Field("Etiologia_CAC").GetString().Trim(' ');
            cacRecord.PesoKG = row.Field("Peso_Kilogramos").GetString().Trim(' ').Length > 5 ? row.Field("Peso_Kilogramos").GetString().Trim(' ').Substring(0, 5) : row.Field("Peso_Kilogramos").GetString().Trim(' ');
            cacRecord.tallaCtms = row.Field("Talla_centimetros").GetString().Trim(' ').Length > 5 ? row.Field("Talla_centimetros").GetString().Trim(' ').Substring(0, 5) : row.Field("Talla_centimetros").GetString().Trim(' ');
            cacRecord.tensionArtSistolica = row.Field("Tension_Arterial_Sistolica_mmHg").GetString().Trim(' ').Length > 5 ? row.Field("Tension_Arterial_Sistolica_mmHg").GetString().Trim(' ').Substring(0, 5) : row.Field("Tension_Arterial_Sistolica_mmHg").GetString().Trim(' ');
            cacRecord.tensionArtDiastolica = row.Field("Tension_Arterial_Diastolica_mmHg").GetString().Trim(' ').Length > 5 ? row.Field("Tension_Arterial_Diastolica_mmHg").GetString().Trim(' ').Substring(0, 5) : row.Field("Tension_Arterial_Diastolica_mmHg").GetString().Trim(' ');
            cacRecord.creatinina = row.Field("Creatinina").GetString().Trim(' ').Length > 5 ? row.Field("Creatinina").GetString().Trim(' ').Substring(0, 5) : row.Field("Creatinina").GetString().Trim(' ');
            cacRecord.hemoglobinaGlicosilada = row.Field("Hemoglobina_Glicosilada").GetString().Trim(' ').Length > 5 ? row.Field("Hemoglobina_Glicosilada").GetString().Trim(' ').Substring(0, 5) : row.Field("Hemoglobina_Glicosilada").GetString().Trim(' ');
            cacRecord.albuminuria = row.Field("Albuminuria").GetString().Trim(' ').Length > 5 ? row.Field("Albuminuria").GetString().Trim(' ').Substring(0, 5) : row.Field("Albuminuria").GetString().Trim(' ');
            cacRecord.creatinuria = row.Field("Creatinuria").GetString().Trim(' ').Length > 5 ? row.Field("Creatinuria").GetString().Trim(' ').Substring(0, 5) : row.Field("Creatinuria").GetString().Trim(' ');
            cacRecord.LDL = row.Field("LDL").GetString().Trim(' ').Length > 5 ? row.Field("LDL").GetString().Trim(' ').Substring(0, 5) : row.Field("LDL").GetString().Trim(' ');
            cacRecord.PTH = row.Field("PTH").GetString().Trim(' ').Length > 5 ? row.Field("PTH").GetString().Trim(' ').Substring(0, 5) : row.Field("PTH").GetString().Trim(' ');
            cacRecord.tasaFiltracionGlomerular = row.Field("Tasa_Filtracion_Glomerular_TFG").GetString().Trim(' ').Length > 5 ? row.Field("Tasa_Filtracion_Glomerular_TFG").GetString().Trim(' ').Substring(0, 5) : row.Field("Tasa_Filtracion_Glomerular_TFG").GetString().Trim(' ');
            cacRecord.diagEnferRenalCronicoERC = row.Field("Diagnostico_Enfermedad_Renal_Cronico_ERC").GetString().Trim(' ').Length > 5 ? row.Field("Diagnostico_Enfermedad_Renal_Cronico_ERC").GetString().Trim(' ').Substring(0, 5) : row.Field("Diagnostico_Enfermedad_Renal_Cronico_ERC").GetString().Trim(' ');
            cacRecord.estadioERC = row.Field("Estadio_ERC").GetString().Trim(' ').Length > 5 ? row.Field("Estadio_ERC").GetString().Trim(' ').Substring(0, 5) : row.Field("Estadio_ERC").GetString().Trim(' ');

            cacRecord.fecUltimaLDL = Convert.ToDateTime(row.Field("Fecha_Ultimo_LDL").GetString());
            cacRecord.fecUltimaCreatinuria = Convert.ToDateTime(row.Field("Fecha_Ultima_Creatinuria").GetString());
            cacRecord.fecHemoGlicosilada = Convert.ToDateTime(row.Field("Fecha_Ultima_Hemoglobina_Glicosilada").GetString());
            cacRecord.fecUltimaAlbuminuria = Convert.ToDateTime(row.Field("Fecha_Ultima_Albuminuria").GetString());
            cacRecord.fechaPTH = Convert.ToDateTime(row.Field("Fecha_PTH").GetString());
            cacRecord.fecUltimaCreatinina = Convert.ToDateTime(row.Field("Fecha_Ultima_Creatinina").GetString());
            cacRecord.fechaNacimiento = Convert.ToDateTime(row.Field("Fecha_Nacimiento").GetString());
            cacRecord.fecDiagHiperArterial = Convert.ToDateTime(row.Field("Fecha_Diagnostico_Hipertension_Arterial").GetString());
            cacRecord.fecDiadDiabetesMellitus = Convert.ToDateTime(row.Field("Fecha_Diagnostico_Diabetes_Mellitus").GetString());
            cacRecord.idOrganizacion = idOrganizacion;
           
            return cacRecord;
        }
    }
}
