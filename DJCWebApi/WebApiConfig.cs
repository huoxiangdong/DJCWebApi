namespace DJCWebApi
{
    using System;
    using System.Configuration;
    using System.Web.Http;
    using System.Web.Http.Cors;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            EnableCorsAttribute defaultPolicyProvider = new EnableCorsAttribute(ConfigurationManager.get_AppSettings()["Access-Control-Allow-Origin"], ConfigurationManager.get_AppSettings()["Access-Control-Allow-Headers"], ConfigurationManager.get_AppSettings()["Access-Control-Allow-Methods"]);
            config.EnableCors(defaultPolicyProvider);
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter("Bearer"));
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}

