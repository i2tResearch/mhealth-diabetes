using CAC.Library.Utilities;
using Indicadores.Interfaces;
using Indicadores.Library.BP.Totales;
using Indicadores.Library.Model.DTO;
using Indicadores.Library.Model.Utilities;
using System;
using System.Collections.Generic;

namespace Indicadores.Services
{
    /// <summary>
    /// Tortas
    /// </summary>
    public class IndicadoresTotalesService : IIndicadoresTotalesService
    {

        public List<DTOIndicadorTotales> ObtenerAlbuminuria(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                Albuminuria obj = new Albuminuria();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerCreatinina(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                Creatinina obj = new Creatinina();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerHbA1c(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                HbA1c obj = new HbA1c();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerHipertensionArterial(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                HipertensionArterial obj = new HipertensionArterial();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerLDL(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                LDL obj = new LDL();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerProgEnfRenal(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                ProgresionEnferRenal obj = new ProgresionEnferRenal();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerPTH3(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                PTH3 obj = new PTH3();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerPTH4(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                PTH4 obj = new PTH4();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }

        public List<DTOIndicadorTotales> ObtenerPTH5(string fechaIni, string fechaFin, string idOrganizacion)
        {
            try
            {
                PTH5 obj = new PTH5();
                return obj.ObtenerIndicador(fechaIni, fechaFin, idOrganizacion);
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<IndicadoresTotalesService>());
                throw ex;
            }
        }
    }
}
