using Indicadores.Library.Model.DB;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Indicadores.Library.BP
{
    public class ControlDiabetesMellitus : CalculoIndicadoresProceso
    {
    
        public override Resultado callProcedure(DateTime fechaInicio, String idOrganizacion)
        {
            List<ConexionProcAlmacenados.Parametros> parametros = new List<ConexionProcAlmacenados.Parametros>();

            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@fechaIni_IN", fechaInicio));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@intervalo_IN",
                Convert.ToInt32(ConfigurationManager.AppSettings["intervalo_meses_control_diabetes_mellitus"])));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@idOrganizacion_IN", idOrganizacion));

            return ConexionProcAlmacenados.GetInstance().ObtenerDatosIndicadores(ConstantesPrcs.PRC_CONTROL_DIABETES_MELLITUS, parametros);
        }
    }
}
