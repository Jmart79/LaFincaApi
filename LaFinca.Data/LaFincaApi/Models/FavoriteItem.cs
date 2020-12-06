using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Models
{
    public class FavoriteItem
    {
        [BsonId]
        public ObjectId id { get; set; }
        [BsonElement("Username")]
        [JsonProperty("Username")]
        public string Username { get; set; }
        [BsonElement("Favorites")]
        [JsonProperty("Favorites")]
        public string[] Favorites { get; set; }
        private List<string> favorites { get; set; }

        public FavoriteItem() { }

        public FavoriteItem(string username, string[] favories)
        {
            this.Username = username;
            this.Favorites = favories;
        }

        public void AddItem(string itemName)
        {
            favorites.Add(itemName);

            Favorites = favorites.ToArray();
        }

        public void RemoveItem(string itemName)
        {
            favorites.Remove(itemName);

            Favorites = favorites.ToArray();
        }


    }
}
