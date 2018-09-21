using Indicadores.Library.Model.DB;
using System;
using System.Collections.Generic;

namespace Indicadores.Library.BP
{
    public class MedicionPTHERC4 : CalculoIndicadoresProceso
    {
        public override Resultado callProcedure(DateTime fechaInicio, String idOrganizacion)
        {
            List<ConexionProcAlmacenados.Parametros> parametros = new List<ConexionProcAlmacenados.Parametros>();

            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@fechaIni_IN", fechaInicio));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@intervalo_IN", Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["intervalo_meses_medicion_pth_semestral"])));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@estadioERC_IN", 4));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@idOrganizacion_IN", idOrganizacion));

            return ConexionProcAlmacenados.GetInstance().ObtenerDatosIndicadores(ConstantesPrcs.PRC_MEDICION_PTH, parametros);
        }
    }
}
