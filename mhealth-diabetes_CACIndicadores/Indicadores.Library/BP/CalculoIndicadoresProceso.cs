using Indicadores.Library.Model.DB;
using Indicadores.Library.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects;

namespace Indicadores.Library.BP
{
    /// <summary>
    /// Barras
    /// </summary>
    public abstract class CalculoIndicadoresProceso
    {

        public abstract Resultado callProcedure(DateTime fechaInicio, String idOrganizacion);

        public List<DTOIndicador> ObtenerIndicador(DateTime fechaInicio, DateTime fechaFin, String idOrganizacion)
        {
            List<DTOIndicador> lstIndicador = new List<DTOIndicador>();
            DTOIndicador dtoIndicador;

            while (fechaInicio <= fechaFin)
            {
                dtoIndicador = new DTOIndicador();
                dtoIndicador.Mes = fechaInicio.Month;
                dtoIndicador.Year = fechaInicio.Year;

                Resultado resultado = callProcedure(fechaInicio, idOrganizacion);

                dtoIndicador.Numerador = resultado.Numerador;
                dtoIndicador.Denominador = resultado.Denominador;
                dtoIndicador.Resultado = resultado.ResultadoIndicador;

                fechaInicio = fechaInicio.AddMonths(1);

                lstIndicador.Add(dtoIndicador);
            }

            return lstIndicador;

        }

        public List<DTOIndicador> ObtenerIndicador(string fechaInicio, string fechaFin, string idOrganizacion)
        {
            DateTime _tempInicio = DateTime.Parse(fechaInicio);
            DateTime _tempFin = DateTime.Parse(fechaFin);
            DateTime _FechaInicio = TimeZoneInfo.ConvertTime(_tempInicio, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            DateTime _FechaFin = TimeZoneInfo.ConvertTime(_tempFin, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            //CAC.Library.Utilities.IOUtilities.WriteLog($"sys:\t{DateTime.Now}\tincome:\t{fechaInicio}\toutput:\t{_FechaInicio.ToLongDateString()}\tincome_ent:\t{fechaFin}\toutput_end:\t{_FechaFin.ToLongDateString()}", "Audit", "dates_audit.txt");
            return ObtenerIndicador(_FechaInicio, _FechaFin, idOrganizacion);

        }



    }
}
