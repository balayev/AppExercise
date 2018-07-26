using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplicationExercise
{
    /// <summary>
    /// ASP.NET MVC route configuration.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers ASP.NET MVC routes.
        /// </summary>
        /// <param name="routes">Collecton of routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
