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
    /// <summary>
    /// API que gestiona los pacientes prioritarios
    /// </summary>
    public class PriorityPatientService : IPriorityPatientService
    {

        private SingletonDB controllerDB = SingletonDB.getInstance();

        //representa la conexión a la base datos inicializando un objeto contexto de Entity Framework
        private ModelContainer db = null;

        public PriorityPatientService()
        {
            db = controllerDB.DB;
        }

        /// <summary>
        /// Obtiene una lista de pacientes prioritarios por el Id del archivo
        /// </summary>
        /// <param name="idArchivo">Id del archivo</param>
        /// <returns>Lista de pacientes prioritarios</returns>
        public List<DTOPacientePrioritario> listPriorityPatient(string idArchivo)
        {
            List<DTOPacientePrioritario> response = new List<DTOPacientePrioritario>();

            try
            {
                PacientePrioritario_Factory factoryPatient = new PacientePrioritario_Factory();

                Guid idparse = Guid.Parse(idArchivo);
                List<tbl_paciente_prioritario> lstModelo = db.tbl_paciente_prioritario.Where(m => m.idArchivo == idparse).ToList();

                if (lstModelo != null && lstModelo.Count > 0)
                {
                    response = factoryPatient.transformListDTO(lstModelo).OrderBy(m => m.Nombres).ToList();
                }
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<PriorityPatientService>());
            }

            return response;
        }
    }
}
