using LaFincaApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }

        public List<Order> Get()
        {
            return _orders.Find(order => true).ToList<Order>();
        }

        public Order GetOrderById(string orderId)
        {
            return _orders.Find(order => order.OrderId == orderId).FirstOrDefault();
        }

        public List<Order> GetOrdersByCustomer(string username)
        {
            return _orders.Find(order => order.CustomerUsername == username).ToList<Order>();
        }

        public List<Order> GetAllCurrentOrders()
        {
            return _orders.Find(order => order.State == "current").ToList<Order>();
        }

       public List<Order> GetAllPastOrders()
        {
            return _orders.Find(order => order.State == "past").ToList<Order>();
        }

        public List<Order> GetAllOrders(string customerUsername)
        {
            return _orders.Find(order => order.CustomerUsername == customerUsername).ToList<Order>();
        }

        public void UpdateOrder(Order updatedOrder)
        {
            Order mergedOrder = MergeOrders(GetOrderById(updatedOrder.OrderId), updatedOrder);
            if (DoesOrderExist(updatedOrder.OrderId))
            {
                _orders.ReplaceOne(child => child.OrderId == updatedOrder.OrderId, updatedOrder);
            }
        }

        public bool Create(Order order)
        {
            bool WasCreated = false;
            if (!DoesOrderExist(order.OrderId))
            {
                _orders.InsertOne(order);
                WasCreated = true;
            }

            return WasCreated;
        }

        public bool Delete(string orderId)
        {
            bool WasDeleted = false;
            if (DoesOrderExist(orderId))
            {
                _orders.DeleteOne(order => order.OrderId == orderId);
                WasDeleted = true;
            }
            return WasDeleted;
        }

        private Order MergeOrders(Order previousOrder, Order updatedOrder)
        {
            if (previousOrder.State != updatedOrder.State) previousOrder.State = updatedOrder.State;
            if (previousOrder.Cost != updatedOrder.Cost) previousOrder.Cost = updatedOrder.Cost;
            if (previousOrder.Items != updatedOrder.Items) previousOrder.Items = updatedOrder.Items;
            if (previousOrder.OrderReady != updatedOrder.OrderReady) previousOrder.OrderReady = updatedOrder.OrderReady;
            return previousOrder;
        }

        private bool DoesOrderExist(string orderId)
        {
            if(GetOrderById(orderId) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
