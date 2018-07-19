using System.Data.Entity;
using WebApplicationExercise.Models;

namespace WebApplicationExercise.Core
{
    /// <summary>
    /// Data context interface for API.
    /// </summary>
    public interface IMainDataContext
    {
        /// <summary>
        /// Collection of orders.
        /// </summary>
        DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Collection of products.
        /// </summary>
        DbSet<Product> Products { get; set; }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        int SaveChanges();
    }
}