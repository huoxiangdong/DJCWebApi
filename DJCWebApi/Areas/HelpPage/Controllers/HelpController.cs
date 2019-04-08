namespace DJCWebApi.Areas.HelpPage.Controllers
{
    using DJCWebApi.Areas.HelpPage;
    using DJCWebApi.Areas.HelpPage.ModelDescriptions;
    using DJCWebApi.Areas.HelpPage.Models;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Web.Http;
    using System.Web.Mvc;

    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HttpConfiguration <Configuration>k__BackingField;

        public HelpController() : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            this.Configuration = config;
        }

        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel helpPageApiModel = this.Configuration.GetHelpPageApiModel(apiId);
                if (helpPageApiModel > null)
                {
                    return base.View(helpPageApiModel);
                }
            }
            return base.View("Error");
        }

        public ActionResult Index()
        {
            if (<>o__7.<>p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                <>o__7.<>p__0 = CallSite<Func<CallSite, object, IDocumentationProvider, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DocumentationProvider", typeof(HelpController), argumentInfo));
            }
            <>o__7.<>p__0.Target(<>o__7.<>p__0, base.ViewBag, this.Configuration.Services.GetDocumentationProvider());
            return base.View(this.Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName) && this.Configuration.GetModelDescriptionGenerator().GeneratedModels.TryGetValue(modelName, out ModelDescription description))
            {
                return base.View(description);
            }
            return base.View("Error");
        }

        public HttpConfiguration Configuration { get; private set; }

        [CompilerGenerated]
        private static class <>o__7
        {
            public static CallSite<Func<CallSite, object, IDocumentationProvider, object>> <>p__0;
        }
    }
}

