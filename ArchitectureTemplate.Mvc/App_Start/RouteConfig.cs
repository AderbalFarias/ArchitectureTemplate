using System.Web.Mvc;
using System.Web.Routing;

namespace ArchitectureTemplate.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Info", "Info/{action}",
                new { controller = "Info", action = "Index" });

            routes.MapRoute("Processo", 
               url: "{controller}/{action}/{processoId}/{id}",
               defaults: new { controller = "Arquivo", action = "Index" },
               constraints: new { processoId = @"\d+"});

            //Funciona
            //routes.MapRoute("Wf", "{controller}/{action}/{id}/{reload}",
            //    new { controller = "Workflow", action = "Index", id = 0, reload = UrlParameter.Optional });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
