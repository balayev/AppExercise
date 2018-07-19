using System.Data.Entity;
using WebApplicationExercise.Models;

namespace WebApplicationExercise.Core
{
    /// <summary>
    /// Data context for API.
    /// </summary>
    public class MainDataContext : DbContext, IMainDataContext
    {
        /// <summary>
        /// Collection of orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Collection of products.
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}