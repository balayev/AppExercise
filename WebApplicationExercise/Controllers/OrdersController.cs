using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebApplicationExercise.Core;
using WebApplicationExercise.Models;

namespace WebApplicationExercise.Controllers
{
    /// <summary>
    /// Provides methods to work with orders.
    /// </summary>
    [RoutePrefix("api/V1/orders")]
    public class OrdersController : ApiController
    {
        private readonly IMainDataContext _dataContext;
        private readonly ICustomerManager _customerManager;

        /// <summary>
        /// Constructor of OrdersController.
        /// </summary>
        /// <param name="dataContext">Database context.</param>
        /// <param name="customerManager">Customer manager.</param>
        public OrdersController(IMainDataContext dataContext, ICustomerManager customerManager)
        {
            _dataContext = dataContext;
            _customerManager = customerManager;
        }

        /// <summary>
        /// Returns order with specified id.
        /// </summary>
        /// <param name="id">Id of order.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Order Order(Guid id)
        {
            return _dataContext.Orders.Include(o => o.Products).Single(o => o.Id == id);
        }

        /// <summary>
        /// Returns list of products filtered by parameters.
        /// </summary>
        /// <param name="from">Filter from date.</param>
        /// <param name="to">Filter to date.</param>
        /// <param name="customerName">Cuctomer name.</param>
        /// <returns>List of products filtered by parameters.</returns>
        [HttpGet]
        [Route]
        public IEnumerable<Order> Orders(DateTime? from = null, DateTime? to = null, string customerName = null)
        {
            IEnumerable<Order> orders = _dataContext.Orders.Include(o => o.Products);

            if (from != null && to != null)
            {
                orders = FilterByDate(orders, from.Value, to.Value);
            }

            if (customerName != null)
            {
                orders = FilterByCustomer(orders, customerName);
            }

            return orders.Where(o => _customerManager.IsCustomerVisible(o.Customer));
        }

        /// <summary>
        /// Saves the order.
        /// </summary>
        /// <param name="order">Order parameters.</param>
        [HttpPost]
        [Route]
        public void Post([FromBody]Order order)
        {
            _dataContext.Orders.Add(order);
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Updates or creates a new the order.
        /// </summary>
        /// <param name="order">Order parameters.</param>
        [HttpPut]
        [Route]
        public void Put([FromBody]Order order)
        {
            var existingOrder = _dataContext.Orders.Where(x => x.Id == order.Id).FirstOrDefault();

            if (existingOrder == null)
            {
                _dataContext.Orders.Add(order);
            }
            else
            {
                existingOrder.Products = order.Products;
                existingOrder.Customer = order.Customer;
                existingOrder.CreatedDate = order.CreatedDate;
            }

            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Removes the order by id.
        /// </summary>
        /// <param name="id">Id of order to remove.</param>
        [HttpDelete]
        [Route("{id}")]
        public void Delete(Guid id)
        {
            var existingOrder = _dataContext.Orders.Where(x => x.Id == id).FirstOrDefault();
            if (existingOrder != null)
            {
                _dataContext.Orders.Remove(existingOrder);
                _dataContext.SaveChanges();
            }
        }

        private IEnumerable<Order> FilterByCustomer(IEnumerable<Order> orders, string customerName)
        {
            return orders.Where(o => o.Customer == customerName);
        }

        private IEnumerable<Order> FilterByDate(IEnumerable<Order> orders, DateTime from, DateTime to)
        {
            return orders.Where(o => o.CreatedDate >= from && o.CreatedDate < to);
        }
    }
}