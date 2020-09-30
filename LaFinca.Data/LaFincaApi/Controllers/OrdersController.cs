using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaFincaApi.Models;
using LaFincaApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaFincaApi.Controllers
{
    
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService service)
        {
            this._orderService = service;
        }

        public ActionResult<List<Order>> ViewAll()
        {
            return _orderService.Get();
        }

        public ActionResult<List<Order>> ViewAllByUser([FromQuery(Name = "username")] string username)
        {
            return _orderService.GetOrdersByCustomer(username);
        }

        public ActionResult<Order> ViewOrder(string id)
        {
            Order order = _orderService.GetOrderById(id);
            return order;
        }

        public ActionResult<List<Order>> ViewAllCurrent()
        {
            return _orderService.GetAllCurrentOrders();
        }

        public ActionResult<List<Order>> ViewAllPast()
        {
            return _orderService.GetAllPastOrders();
        }

        public IActionResult MarkOrderAsReady(string id)
        {
            Order order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            order.State = "ready";
            _orderService.UpdateOrder(order);
            return NoContent();
        }

        public IActionResult MarkOrderAsComplete(string id)
        {
            Order order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            order.State = "past";
            _orderService.UpdateOrder(order);
            return NoContent();
        }



        public IActionResult Update(Order updatedOder)
        {
            if(_orderService.GetOrderById(updatedOder.OrderId) != null)
            {
                _orderService.UpdateOrder(updatedOder);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }



    }
}
