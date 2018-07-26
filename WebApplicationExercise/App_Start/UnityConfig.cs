using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace WebApplicationExercise
{
    /// <summary>
    /// Configuration of ASP.NET MVC.
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterInstance<HttpConfiguration>(GlobalConfiguration.Configuration);
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}