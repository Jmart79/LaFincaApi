﻿
using LaFincaApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaFincaApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<IUser> _users;
        
        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<IUser>(settings.UsersCollectionName);
        }

        public List<IUser> Get()
        {
            List<IUser> users = _users.Find(user => true).ToList<IUser>();

            return users;
        }

        public IUser Get(string id=null,string email = null)
        {
            IUser user = null;
            if(id != null) user=  _users.Find(user => user.username == id).FirstOrDefault();
            if(email != null) user= _users.Find(user => user.email == email).FirstOrDefault();

            return user;
        }

        

        public bool Create(IUser user)
        {
            bool wasCreated = false;
            if (!DoesUserExist(user.username))
            {
                _users.InsertOne(user);
                wasCreated = true;
            }
            return wasCreated;
        }

        public bool Update(string id, IUser updatedUser)
        {
            bool ResponseMessage = false;
            if(DoesUserExist(username: id))
            {
                _users.ReplaceOne(user => user.username == id, updatedUser);
                ResponseMessage = true;
            }
            return ResponseMessage;
        }

        public void Remove(IUser user) =>
            _users.DeleteOne(child => child.username == user.username);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.username == id);

        public bool DoesUserExist( string username = null,string email = null)
        {
            bool userExists = false;
         
            if(username != null)
            {
                IUser foundUser = Get(id: username,email: email);
                if(foundUser != null)
                {
                    userExists = true;
                }
                 foundUser = null;
            }
            if (email != null)
            {
                IUser foundUser = Get(email);
                if (foundUser != null)
                {
                    userExists = true;
                }
                foundUser = null;
            }
            return userExists;
        }

    }
}
