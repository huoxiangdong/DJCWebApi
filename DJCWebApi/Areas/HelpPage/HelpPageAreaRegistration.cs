namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Web.Http;
    using System.Web.Mvc;

    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("HelpPage_Default", "Help/{action}/{apiId}", new { 
                controller = "Help",
                action = "Index",
                apiId = UrlParameter.Optional
            });
            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }

        public override string AreaName =>
            "HelpPage";
    }
}

