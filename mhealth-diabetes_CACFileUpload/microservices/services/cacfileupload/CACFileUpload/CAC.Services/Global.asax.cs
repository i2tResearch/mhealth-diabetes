using System;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace CAC.Services
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("api/cac/v1/prioritypatient", new WebServiceHostFactory(),typeof(PriorityPatientService)));
            RouteTable.Routes.Add(new ServiceRoute("api/cac/v1/user", new WebServiceHostFactory(), typeof(User)));
            RouteTable.Routes.Add(new ServiceRoute("api/cac/v1/cac", new WebServiceHostFactory(), typeof(CACService)));
            RouteTable.Routes.Add(new ServiceRoute("api/cac/v1/file", new WebServiceHostFactory(), typeof(FileService)));            
            RouteTable.Routes.Add(new ServiceRoute("api/cac/v1/fake", new WebServiceHostFactory(), typeof(Service1)));
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