using CAC.Library.Utilities;
using Indicadores.Interfaces;
using Indicadores.Library.BP;
using Indicadores.Library.Model.DTO;
using Indicadores.Library.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Indicadores.Services
{
    /// <summary>
    /// Barras
    /// </summary>
    public class IndicadorService : IIndicadoresService
    {

        public List<DTOIndicador> ObtenerControlDiabetesMellitus(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                ControlDiabetesMellitus obj = new ControlDiabetesMellitus();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerControlHipertensionArterial(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                ControlHipertensionArterialIndicador objControl = new ControlHipertensionArterialIndicador();
                return objControl.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }

        }

        public List<DTOIndicador> ObtenerControlLDL(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                ControlLDL obj = new ControlLDL();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerControlPTH3(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                ControlPTHERC3 obj = new ControlPTHERC3();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerControlPTH4(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                ControlPTHERC4 obj = new ControlPTHERC4();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerControlPTH5(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                ControlPTHERC5 obj = new ControlPTHERC5();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerMedicionAlbuminuria(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                MedicionAlbuminuria obj = new MedicionAlbuminuria();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerMedicionCreatina(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                MedicionTiempoCreatinina obj = new MedicionTiempoCreatinina();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            };
        }

        public List<DTOIndicador> ObtenerMedicionHbA1c(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                MedicionHbA1 obj = new MedicionHbA1();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerMedicionLDL(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                MedicionLDL obj = new MedicionLDL();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerMedicionPTH3(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                MedicionPTHERC3 obj = new MedicionPTHERC3();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerMedicionPTH4(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                MedicionPTHERC4 obj = new MedicionPTHERC4();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerMedicionPTH5(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                MedicionPTHERC5 obj = new MedicionPTHERC5();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }

        public List<DTOIndicador> ObtenerProgresionRenal(string fechaIni, string fechaFin, String idOrganizacion)
        {
            try
            {
                ProgresionEnfermedadRenal obj = new ProgresionEnfermedadRenal();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadorService>());
                throw ex;
            }
        }
    }
}
