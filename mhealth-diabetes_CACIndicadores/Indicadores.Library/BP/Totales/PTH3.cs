using Indicadores.Library.Model.DB;
using System;
using System.Collections.Generic;

namespace Indicadores.Library.BP.Totales
{
    public class PTH3 : CalculoIndicadores
    {
        public override ResultadoIndicador callProcedure(DateTime fechaInicio, String idOrganizacion)
        {
            List<ConexionProcAlmacenados.Parametros> parametros = new List<ConexionProcAlmacenados.Parametros>();

            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@fechaIni_IN", fechaInicio));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@intervalo_IN",
                Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["intervalo_meses_control_pth_trimestral"])));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@estadioERC_IN", 3));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@PTHMayor_IN", 35));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@PTHMenor_IN", 70));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@idOrganizacion_IN", idOrganizacion));

            return ConexionProcAlmacenados.GetInstance().ObtenerDatosIndicadoresTotales(ConstantesPrcs.PRC_PTH, parametros);
        }
    }
}
