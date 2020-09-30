using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Models
{
    public class MenuItem
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("ItemName")]
        [JsonProperty("ItemName")]
        public string ItemName { get; set; }

        [BsonElement("Category")]
        [JsonProperty("Category")]
        public string Category { get; set; }

        [BsonElement("Description")]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [BsonElement("Cost")]
        [JsonProperty("Cost")]
        public double Cost { get; set; }

        [BsonElement("IsAvailable")]
        [JsonProperty("IsAvailable")]
        public bool IsAvailable { get; set; }

        [BsonElement("IsHouseFavorite")]
        [JsonProperty("IsHouseFavorite")]
        public bool IsHouseFavorite { get; set; }

        public MenuItem() { }

        public MenuItem(string name, string description,string category, double cost, bool available = true, bool houseFavorite = false)
        {
            this.ItemName = name;
            this.Description = description;
            this.Category = category;
            this.Cost = cost;
            this.IsAvailable = available;
            this.IsHouseFavorite = houseFavorite;
        }
    }
}
