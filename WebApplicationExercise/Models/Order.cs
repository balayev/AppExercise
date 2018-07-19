using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationExercise.Models
{
    /// <summary>
    /// Order representation.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Date of order creation.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Customer name.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Collection of products.
        /// </summary>
        public List<Product> Products { get; set; }
    }
}