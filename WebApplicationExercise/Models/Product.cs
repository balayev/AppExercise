using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationExercise.Models
{
    /// <summary>
    /// Product representation.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public double Price { get; set; }
    }
}