using AzureFunction.CRUD.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureFunction.CRUD.Infrastructure
{
    internal class MongoDBService
    {
        private IMongoCollection<User> _mongoCollection { get; }

        public MongoDBService()
        {
            MongoClient mongoClient = new MongoClient(Environment.GetEnvironmentVariable("CRUDDBConection", EnvironmentVariableTarget.Process));
            var database = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("CRUDDBName", EnvironmentVariableTarget.Process));
            _mongoCollection = database.GetCollection<User>(Environment.GetEnvironmentVariable("UserCollectionName", EnvironmentVariableTarget.Process));
        }
        public void Create(User user)
        {
            _mongoCollection.InsertOneAsync(user);
        }
        public void Update(User user)
        {
            var filters = new[]
            {
                Builders<User>.Filter.Eq("_id", user.Id)
            };
            var filter = Builders<User>.Filter.And(filters);
            ReplaceOptions options = new ReplaceOptions()
            {
                IsUpsert = true
            };
             _mongoCollection.ReplaceOne(filter, user, options);
        }
        public async Task<User> Read(Guid id)
        {
            var filters = new[]
            {
                Builders<User>.Filter.Eq("_id", id)
            };
            var filter = Builders<User>.Filter.And(filters);
            return await _mongoCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
        public async Task<List<User>> Read()
        {
            var filters = new[]
            {
                Builders<User>.Filter.Empty
            };
            var filter = Builders<User>.Filter.And(filters);
            List<User> users = await _mongoCollection.Find(filter).ToListAsync();
            return users;
        }
        public async Task<DeleteResult> Remove(Guid id)
        {
            var filters = new[]
            {
                Builders<User>.Filter.Eq("_id", id)
            };
            var filter = Builders<User>.Filter.And(filters);
            return await _mongoCollection.DeleteOneAsync(filter);
        }
    }
}
