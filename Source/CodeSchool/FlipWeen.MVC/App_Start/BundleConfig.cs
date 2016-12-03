using System.Web;
using System.Web.Optimization;

namespace FlipWeen.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-form.js",
                      "~/Scripts/jquery.sidr.min.js",
                      "~/Scripts/jquery.touchwipe.min.js"
                      
                        ));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                     "~/Scripts/md_slider.min.js",
                     "~/Scripts/raphael-min.js",
                     "~/Scripts/responsiveslides.min.js",
                     "~/Scripts/script.js",
                     "~/Scripts/selectnav.min.js",
                     "~/Scripts/pie.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     
                      "~/Content/bootstrap.css",
                      "~/Content/normalize.css",
                      "~/Content/jquery.sidr.light.css",
                      "~/Content/animate.min.css",
                      "~/Content/md-slider.css",
                      "~/Content/responsive.css",
                      "~/Content/responsiveslides.css",
                      "~/Content/flipween.css",
                      "~/Content/style.css",
                      "~/Content/main.css"
                      
                      ));
        }
    }
}
