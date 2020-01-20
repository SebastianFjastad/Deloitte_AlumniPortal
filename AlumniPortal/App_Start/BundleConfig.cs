using System.Web;
using System.Web.Optimization;

namespace AlumniPortal
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                "~/Scripts/moment.min.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/tinymce"));

            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
                "~/Scripts/App/Calendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/imageupload").Include(
               "~/Scripts/App/ImageUpload.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datetimepicker.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/basic.css",
                     "~/Scripts/dropzone/dropzone.css"));

            //bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
            //    "~/Scripts/DataTables/", "*.min.js"));

            //bundles.Add(new StyleBundle("~/Content/DataTables").Include(
            //                     "~/Content/DataTables/css", "*.min.css"));

        }
    }
}
