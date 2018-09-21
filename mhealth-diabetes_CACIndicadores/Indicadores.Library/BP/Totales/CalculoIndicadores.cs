using Indicadores.Library.Model.DB;
using Indicadores.Library.Model.DTO;
using System;
using System.Collections.Generic;

namespace Indicadores.Library.BP
{
    /// <summary>
    /// Tortas
    /// </summary>
    public abstract class CalculoIndicadores
    {

        public abstract ResultadoIndicador callProcedure(DateTime fechaInicio, String idOrganizacion);

        public List<DTOIndicadorTotales> ObtenerIndicador(DateTime fechaInicio, DateTime fechaFin, String idOrganizacion)
        {
            List<DTOIndicadorTotales> lstIndicador = new List<DTOIndicadorTotales>();
            DTOIndicadorTotales dtoIndicador;

            while (fechaInicio <= fechaFin)
            {
                dtoIndicador = new DTOIndicadorTotales();
                dtoIndicador.Mes = fechaInicio.Month;

                ResultadoIndicador resultado = callProcedure(fechaInicio, idOrganizacion);

                dtoIndicador.NoMedidos = resultado.NoMedidos;
                dtoIndicador.PacientesControlados = resultado.PacientesControlados;
                dtoIndicador.PacientesEstadios = resultado.PacientesEstadios;
                dtoIndicador.PacientesEstudiados = resultado.PacientesEstudiados;
                dtoIndicador.VigentesControlados = resultado.VigentesControlados;
                dtoIndicador.VigentesDescontrolados = resultado.VigentesDescontrolados;

                fechaInicio = fechaInicio.AddMonths(1);

                lstIndicador.Add(dtoIndicador);
            }

            return lstIndicador;

        }

        public List<DTOIndicadorTotales> ObtenerIndicador(string fechaInicio, string fechaFin, string idOrganizacion)
        {
            DateTime _tempInicio = DateTime.Parse(fechaInicio);
            DateTime _tempFin = DateTime.Parse(fechaFin);
            DateTime _FechaInicio = TimeZoneInfo.ConvertTime(_tempInicio, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            DateTime _FechaFin = TimeZoneInfo.ConvertTime(_tempFin, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            //CAC.Library.Utilities.IOUtilities.WriteLog($"sys:\t{DateTime.Now}\tincome:\t{fechaInicio}\toutput:\t{_FechaInicio.ToLongDateString()}\tincome_ent:\t{fechaFin}\toutput_end:\t{_FechaFin.ToLongDateString()}", "Audit", "dates_audit.txt");
            return ObtenerIndicador(_FechaInicio, _FechaInicio, idOrganizacion);

        }

    }
}
