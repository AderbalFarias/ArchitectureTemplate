using System.Collections.Generic;
using System.Web.Optimization;

namespace ArchitectureTemplate.Mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));


            var oPlugins = new ScriptBundle("~/bundles/plugins")
                .Include("~/wwwroot/plugins/jquery-2.1.4.min.js",
                    "~/wwwroot/plugins/jquery.easing.1.3.js",
                    "~/wwwroot/plugins/jquery.cookie.js",
                    "~/wwwroot/plugins/jquery.appear.js",
                    "~/wwwroot/plugins/jquery.isotope.js",
                    "~/wwwroot/plugins/bootstrap/js/bootstrap.min.js");
            bundles.Add(oPlugins);

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive")
                .Include("~/wwwroot/plugins/jquery/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker")
                .Include("~/wwwroot/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/mask")
                .Include("~/wwwroot/plugins/jquery-mask/jquery.mask.js"));

            var oJqueryval = new ScriptBundle("~/bundles/jqueryval")
                .Include("~/wwwroot/plugins/jquery-validation/jquery.validate-vsdoc.js",
                    "~/wwwroot/plugins/jquery-validation/jquery.validate.js",
                    "~/wwwroot/plugins/jquery-validation/additional-methods.js",
                    "~/wwwroot/plugins/jquery-validation/jquery.validate.unobtrusive.min.js",
                    "~/wwwroot/plugins/globalize/globalize.js",
                    "~/wwwroot/plugins/globalize/cultures/globalize.culture.pt-BR.js",
                    "~/wwwroot/plugins/jquery-validation/jquery.validate.globalize.js",
                    "~/wwwroot/js/pt-Br.js");

            oJqueryval.Orderer = new AsIsBundleOrderer();
            bundles.Add(oJqueryval);

            var oHighcharts = new ScriptBundle("~/bundles/highcharts")
                .Include("~/wwwroot/plugins/highcharts/highcharts.js",
                    "~/wwwroot/plugins/highcharts/highcharts_pt-Br.js",
                    "~/wwwroot/plugins/highcharts/highcharts-3d.js");
            //"~/wwwroot/plugins/highcharts/exporting.js");

            oHighcharts.Orderer = new AsIsBundleOrderer();
            bundles.Add(oHighcharts);

            var oDatetimepicker = new ScriptBundle("~/bundles/datetimepicker")
                .Include("~/wwwroot/plugins/moment/moment.min.js",
                    "~/wwwroot/plugins/bootstrap-datetimepicker/js/pt-br.js",
                    "~/wwwroot/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                    "~/wwwroot/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js");

            oDatetimepicker.Orderer = new AsIsBundleOrderer();
            bundles.Add(oDatetimepicker);

            BundleTable.EnableOptimizations = false;
        }
    }
}


public class AsIsBundleOrderer : IBundleOrderer
{
    public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
    {
        return files;
    }
}
