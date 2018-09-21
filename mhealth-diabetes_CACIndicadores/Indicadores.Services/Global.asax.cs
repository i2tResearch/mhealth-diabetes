using System;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace Indicadores.Services
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("api/cac/v1/indicadores", new WebServiceHostFactory(), typeof(IndicadorService)));
            RouteTable.Routes.Add(new ServiceRoute("api/cac/v1/indicadorestotales", new WebServiceHostFactory(), typeof(IndicadoresTotalesService)));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}