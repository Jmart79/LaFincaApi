using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Models
{
    public class Order
    {
        public ObjectId Id { get; set; }
        public double Cost { get; set; }
        public double FinalCost { get; set; }
        public string OrderId { get; set; }
        public string CustomerUsername { get; set; }        
        public string OrderPlaced { get; set; }
        public string OrderReady { get; set; } = null;
        public string State { get; set; }
        public List<string> ItemNames { get; set; }

        public Order() { }

        public Order(string id, string username, string orderPlaced, string state, double cost, List<string> names)
        {
            this.OrderId = id;
            this.CustomerUsername = username;
            this.OrderPlaced = orderPlaced;
            this.State = state;
            this.Cost = cost;
            this.FinalCost = cost * .0625;
            this.ItemNames = names;
        }

    }
}
