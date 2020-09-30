using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Models
{
   public interface IDatabaseSettings
    {
        string ItemsCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string OrdersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class LaFincaDatabaseSettings : IDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ItemsCollectionName { get; set; }
        public string OrdersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public LaFincaDatabaseSettings()
        {
            UsersCollectionName = "Users";
            ItemsCollectionName = "MenuItems";
            OrdersCollectionName = "Orders";
            ConnectionString = "mongodb://localhost:27017";
            DatabaseName = "LaFinca";
        }

    }
}
