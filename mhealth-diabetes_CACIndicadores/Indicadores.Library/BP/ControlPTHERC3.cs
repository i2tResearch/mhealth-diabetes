using CAC.Library.Utilities;
using Indicadores.Library.Model.DB;
using System;
using System.Collections.Generic;

namespace Indicadores.Library.BP
{
    public class ControlPTHERC3 : CalculoIndicadoresProceso
    {
        public override Resultado callProcedure(DateTime fechaInicio, String idOrganizacion)
        {
            List<ConexionProcAlmacenados.Parametros> parametros = new List<ConexionProcAlmacenados.Parametros>();

            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@fechaIni_IN", fechaInicio));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@intervalo_IN", Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["intervalo_meses_control_pth_anual"])));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@estadioERC_IN", 3));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@PTHMayor_IN", 35));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@PTHMenor_IN", 70));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@idOrganizacion_IN", idOrganizacion));

            return ConexionProcAlmacenados.GetInstance().ObtenerDatosIndicadores(ConstantesPrcs.PRC_CONTROL_PTH, parametros);
        }

    }
}
