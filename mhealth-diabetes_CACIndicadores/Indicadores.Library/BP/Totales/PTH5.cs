using Indicadores.Library.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indicadores.Library.BP.Totales
{
    public class PTH5 : CalculoIndicadores
    {
        public override ResultadoIndicador callProcedure(DateTime fechaInicio, String idOrganizacion)
        {
            List<ConexionProcAlmacenados.Parametros> parametros = new List<ConexionProcAlmacenados.Parametros>();

            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@fechaIni_IN", fechaInicio));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@intervalo_IN",
                Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["intervalo_meses_control_pth_anual"])));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@estadioERC_IN", 5));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@PTHMayor_IN", 150));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@PTHMenor_IN", 300));
            parametros.Add(ConexionProcAlmacenados.Parametros.Build("@idOrganizacion_IN", idOrganizacion));

            return ConexionProcAlmacenados.GetInstance().ObtenerDatosIndicadoresTotales(ConstantesPrcs.PRC_PTH, parametros);
        }
    }
}
