using System.Web.Http;
using Unity;
using Unity.WebApi;
using WebApplicationExercise.Controllers;
using WebApplicationExercise.Core;

namespace WebApplicationExercise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<ICustomerManager, CustomerManager>();
            container.RegisterType<IMainDataContext, MainDataContext>();
            container.RegisterType<OrdersController, OrdersController>();
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
