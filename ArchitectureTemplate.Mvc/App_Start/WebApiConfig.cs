using System.Web.Http;

namespace ArchitectureTemplate.Mvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Habilitando Cors (Cross-Origin Resource Sharing)
            //config.EnableCors();

            //// Web API routes
            config.MapHttpAttributeRoutes();

            ////Remove xml format because it's default
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                "MapByAction",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}
