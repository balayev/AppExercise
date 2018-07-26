using System.Web.Mvc;

namespace WebApplicationExercise
{
    /// <summary>
    /// ASP.NET MVC filters configuration.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers additional global filters.
        /// </summary>
        /// <param name="filters">Collection of global filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
