using LaFincaApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Services
{
    public class FavoriteItemService
    {
        private readonly IMongoCollection<FavoriteItem> _favorites;

        public FavoriteItemService(IDatabaseSettings settings)
        {
            IMongoDatabase db = GetDatabase(settings);
            _favorites = db.GetCollection<FavoriteItem>(settings.FavoriteItemsCollectionName);
        }

        private IMongoDatabase GetDatabase(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            IMongoDatabase db = client.GetDatabase(settings.DatabaseName);

            return db;
        }

        public List<FavoriteItem> GetFavorites(string username)
        {
            return null;
        }

    }
}
