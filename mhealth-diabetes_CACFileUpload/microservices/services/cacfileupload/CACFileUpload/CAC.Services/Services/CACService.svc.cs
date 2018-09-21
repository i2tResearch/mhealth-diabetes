using CAC.Interfaces;
using CAC.Library.Model.DB;
using CAC.Library.Model.DTO;
using CAC.Library.Model.Transform;
using CAC.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAC.Services
{
    public class CACService : ICACService
    {
        private SingletonDB controllerDB = SingletonDB.getInstance();
        //representa la conexión a la base datos inicializando un objeto contexto de Entity Framework
        private ModelContainer db = null;

        public CACService()
        {
            db = controllerDB.DB;
        }

        public DTOArchivo GetFileById(string id)
        {
            DTOArchivo response = null;
            try
            {
                Archivo_Factory factory = new Archivo_Factory();
                Guid idparse = Guid.Parse(id);
                tbl_archivo_cac modelo = db.tbl_archivo_cac.Where(m => m.id == idparse).FirstOrDefault();
                if (modelo != null)
                {
                    response = factory.transformDTO(modelo);
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<CACService>());
            }

            return response;
        }

        public List<DTOPacientePrioritario> listPriorityPatient(String idArchivo)
        {
            List<DTOPacientePrioritario> response = new List<DTOPacientePrioritario>();

            try
            {
                PacientePrioritario_Factory factoryPatient = new PacientePrioritario_Factory();
                VariablePrioritaria_Factory factoryVariable = new VariablePrioritaria_Factory();

                Guid idparse = Guid.Parse(idArchivo);
                List<tbl_paciente_prioritario> lstModelo =
                    db.tbl_paciente_prioritario.Where(m => m.idArchivo == idparse).ToList();

                if (lstModelo != null && lstModelo.Count > 0)
                {
                    response = factoryPatient.transformListDTO(lstModelo);
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<CACService>());
            }

            return response;
        }
    }
}
