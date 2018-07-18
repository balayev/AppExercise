using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;
using WebApplicationExercise.Core;

namespace WebApplicationExercise
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<CustomerManager, CustomerManager>();
            container.RegisterInstance(new MainDataContext());
            container.RegisterInstance<HttpConfiguration>(GlobalConfiguration.Configuration);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}