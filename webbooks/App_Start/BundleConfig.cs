using System.Web;
using System.Web.Optimization;

namespace webbooks
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/metis").Include(
                      "~/Scripts/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/sb-admin-2").Include(
                      "~/Scripts/sb-admin-2.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Content/vendor/datatables/js/jquery.dataTables.min.js",
                      "~/Content/vendor/datatables-plugins/dataTables.bootstrap.min.js",
                      "~/Content/vendor/datatables-responsive/dataTables.responsive.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",                      
                      "~/Content/metisMenu.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/site.css"));
        }
    }
}
