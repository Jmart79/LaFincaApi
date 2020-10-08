using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Models
{
    public class MenuItemService
    {
        private  IMongoCollection<MenuItem> _items;
        private readonly IDatabaseSettings _settings;

        public MenuItemService(IDatabaseSettings settings)
        {
            IMongoDatabase database = GetDatabase();
            _settings = settings;

            SwitchToMenu();
        }

        private IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(_settings.ConnectionString);
            IMongoDatabase db = client.GetDatabase(_settings.DatabaseName);

            return db;
        }

        private void SwitchToFavorites() 
        {
            IMongoDatabase db = GetDatabase();
            _items = db.GetCollection<MenuItem>(_settings.FavoriteItemsCollectionName);
        }

        private void SwitchToMenu() 
        {
            IMongoDatabase db = GetDatabase();
            _items = db.GetCollection<MenuItem>(_settings.ItemsCollectionName);
        }

        public List<MenuItem> Get()
        {
            return _items.Find(item => true).ToList<MenuItem>();
        }

        public MenuItem GetByName(string name)
        {
            return _items.Find(item => item.ItemName == name).FirstOrDefault();
        }

        public List<MenuItem> GetByCategory(string category)
        {
            return _items.Find(item => item.Category == category).ToList<MenuItem>();
        }

        public List<MenuItem> GetByPrice(double cost)
        {
            return _items.Find(item => item.Cost == cost).ToList<MenuItem>();
        }
        
        public List<MenuItem> GetByRange(double lowerBound, double upperBound)
        {
            return _items.Find(item => item.Cost >= lowerBound && item.Cost <= upperBound).ToList<MenuItem>();
        }

        public bool Create(MenuItem item)
        {
            if (!DoesItemExist(item.ItemName))
            {
                _items.InsertOne(item);
                return true;
            }
            return false;
        }

        public bool Update(string itemName, MenuItem updatedItem)
        {
            if (DoesItemExist(itemName))
            {
                _items.ReplaceOne(item => item.ItemName == itemName, updatedItem);
                return true;
            }
            return false;
        }

        public void Remove(string itemName = null, MenuItem item = null)
        {
            if (!DoesItemExist(itemName))
            {
                _items.DeleteOne(item => item.ItemName == itemName);
            }
        }

        public bool DoesItemExist(string itemName)
        {
            bool exists = false;

            MenuItem item = GetByName(itemName);
            if(item != null)
            {
                exists = true;
            }

            return exists;

        }



    }
}
