using System.Web.Http;
using System.Web.Http.Tracing;
using Unity;
using Unity.WebApi;
using WebApplicationExercise.Controllers;
using WebApplicationExercise.Core;

namespace WebApplicationExercise
{
    /// <summary>
    /// Configuration of Web API.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers custom behaviour of http.
        /// </summary>
        /// <param name="config">Http configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            UnityContainer container = RegisterDependencies();
            config.DependencyResolver = new UnityDependencyResolver(container);
            config.Services.Replace(typeof(ITraceWriter), container.Resolve<SimpleTracer>());
            config.MessageHandlers.Add(new ActionLoggingHandler(container.Resolve<SimpleTracer>()));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        /// <summary>
        /// Registers types to Unity container.
        /// </summary>
        /// <returns>Unity container with type registrations.</returns>
        private static UnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();
            container.RegisterType<ICustomerManager, CustomerManager>()
                .RegisterType<IMainDataContext, MainDataContext>()
                .RegisterType<OrdersController, OrdersController>()
                .RegisterType<ILoggerProvider, LoggerProvider>()
                .RegisterType<SimpleTracer, SimpleTracer>();

            return container;
        }
    }
}
