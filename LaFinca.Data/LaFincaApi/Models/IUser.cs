using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Models
{
    public class IUser
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("username")]
        [JsonProperty("username")]
        public string username { get; set; }
        [BsonElement("name")]
        [JsonProperty("name")]
        public string name { get; set; }
        [BsonElement("email")]
        [JsonProperty("email")]
        public string email { get; set; }
        [BsonElement("password")]
        [JsonProperty("password")]
        public string password { get; set; }
        [BsonElement("role")]
        [JsonProperty("role")]
        public string role { get; set; }

        public IUser()
        {

        }

        public IUser( string name, string username, string email,string password, string role)
        {
            this.name = name;
            this.Id = new ObjectId() ;
            this.username = username;
            this.email = email;
            this.password = password;
            this.role = role;
        }
    }
}
