using Indicadores.Library.Model.DB;
using System;
using System.Collections.Generic;

namespace Indicadores.Library.BP.Totales
{
    public class Creatinina : CalculoIndicadores
    {
        public override ResultadoIndicador callProcedure(DateTime fechaInicio, string idOrganizacion)
        {
            List<ConexionProcAlmacenados.Parametros> parametros = new List<ConexionProcAlmacenados.Parametros>();

            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@fechaIni_IN", fechaInicio));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@intervalo_IN",
                Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["intervalo_meses_medicion_creatinina_anual"])));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@idOrganizacion_IN", idOrganizacion));

            return ConexionProcAlmacenados.GetInstance().ObtenerDatosIndicadoresTotales(ConstantesPrcs.PRC_CREATININA, parametros);
        }
    }
}
