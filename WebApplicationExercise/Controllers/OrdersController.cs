using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplicationExercise.Core;
using WebApplicationExercise.Models;
using System.Data.Entity;

namespace WebApplicationExercise.Controllers
{
    [RoutePrefix("api/V1/orders")]
    public class OrdersController : ApiController
    {
        private readonly MainDataContext _dataContext;
        private readonly CustomerManager _customerManager;

        public OrdersController(MainDataContext dataContext, CustomerManager customerManager)
        {
            _dataContext = dataContext;
            _customerManager = customerManager;
        }

        [HttpGet]
        [Route("{id}")]
        public Order Order(Guid id)
        {
            return _dataContext.Orders.Include(o => o.Products).Single(o => o.Id == id);
        }

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

        [HttpPost]
        [Route]
        public void Post([FromBody]Order order)
        {
            _dataContext.Orders.Add(order);
            _dataContext.SaveChanges();
        }

        [HttpPut]
        [Route]
        public void Put([FromBody]Order order)
        {

            _dataContext.Orders.Add(order);
            _dataContext.SaveChanges();
        }

        [HttpDelete]
        [Route]
        public void Delete(Guid orderId)
        {
            _dataContext.Orders.Where( o => o.Id.Equals(orderId));
            _dataContext.SaveChanges();
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