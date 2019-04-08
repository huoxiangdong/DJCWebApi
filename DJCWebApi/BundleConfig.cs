namespace DJCWebApi
{
    using System;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", new IItemTransform[0]));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*", new IItemTransform[0]));
            string[] virtualPaths = new string[] { "~/Scripts/bootstrap.js", "~/Scripts/respond.js" };
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(virtualPaths));
            string[] textArray2 = new string[] { "~/Content/bootstrap.css", "~/Content/site.css" };
            bundles.Add(new StyleBundle("~/Content/css").Include(textArray2));
        }
    }
}

