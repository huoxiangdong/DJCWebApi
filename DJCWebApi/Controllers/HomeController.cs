namespace DJCWebApi.Controllers
{
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (<>o__0.<>p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                <>o__0.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof(HomeController), argumentInfo));
            }
            <>o__0.<>p__0.Target(<>o__0.<>p__0, base.ViewBag, "Home Page");
            return base.View();
        }

        [CompilerGenerated]
        private static class <>o__0
        {
            public static CallSite<Func<CallSite, object, string, object>> <>p__0;
        }
    }
}

