namespace DJCWebApi
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_BeginRequest()
        {
            if (base.Request.Headers.AllKeys.Contains<string>("Origin") && (base.Request.HttpMethod == "OPTIONS"))
            {
            }
            Debug.WriteLine("Request Method:" + base.Request.HttpMethod);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(new Action<HttpConfiguration>(WebApiConfig.Register));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

