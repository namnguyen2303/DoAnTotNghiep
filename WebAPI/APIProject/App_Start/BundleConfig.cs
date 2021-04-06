using System.Web;
using System.Web.Optimization;

namespace APIProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/js/jquery.min.js",
                      "~/Scripts/js/bootstrap.js",
                      "~/Scripts/js/sequence.js",
                      "~/Scripts/js/sequence-theme.modern-slide-in.js",
                      "~/Scripts/js/jquery.simpleGallery.js",
                      "~/Scripts/js/jquery.simpleLens.js",
                      "~/Scripts/js/jquery.smartmenus.js",
                      "~/Scripts/js/jquery.smartmenus.bootstrap.js",
                      "~/Scripts/js/slick.js",
                      "~/Scripts/js/nouislider.js",
                      "~/Scripts/js/custom.js"
                      ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/jquery.smartmenus.bootstrap.css",
                      "~/Content/css/jquery.simpleLens.css",
                      /*"~/Content/css/jquery.simpleGallery.css",*/
                      "~/Content/css/slick.css",
                      "~/Content/css/nouislider.css",
                      "~/Content/css/theme-color/default-theme.css",
                      "~/Content/css/sequence-theme.modern-slide-in.css",
                      "~/Content/css/style.css"
                      ));
            bundles.Add(new ScriptBundle("~/layout/js").Include(
                     "~/Scripts/jquery.min.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/sweet-alert.js",
                     "~/Scripts/lightbox.js",
                     "~/Scripts/lightslider.js",
                     "~/Scripts/owl.carousel.min.js",
                     "~/Scripts/owl.rows.js"
                     ));

            bundles.Add(new ScriptBundle("~/dekko/js").Include(
                     "~/Content/ckfinder/ckfinder.js",
                     "~/Content/ckeditor/ckeditor.js",
                     "~/Scripts/jquery-ui.js",
                     "~/Scripts/ready.js",
                     "~/Scripts/ajax.js",
                     "~/Scripts/jquery.validate.min.js"
                     ));

            bundles.Add(new StyleBundle("~/dekko/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/PagedList.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/main.css",
                      "~/Content/color_skins.css",
                      "~/Content/style.css",
                      "~/Content/login.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/lightbox.css",
                      "~/Content/lightslider.css"
                      ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
