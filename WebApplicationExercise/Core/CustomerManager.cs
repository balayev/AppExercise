﻿namespace WebApplicationExercise.Core
{
    /// <summary>
    /// Provides methods to work with customers.
    /// </summary>
    public class CustomerManager : ICustomerManager
    {
        /// <summary>
        /// Shows is it allowed to work with customer.
        /// </summary>
        /// <param name="customerName">Customer name.</param>
        /// <returns>Is it allowed to work with customer.</returns>
        public bool IsCustomerVisible(string customerName)
        {
            return customerName != "Hidden Joe";
        }
    }
}