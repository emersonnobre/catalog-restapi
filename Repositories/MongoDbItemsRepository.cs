using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName); // pega o banco de dados para manipulação a partir do client
            itemsCollection = database.GetCollection<Item>(collectionName); // pega a collection (tabela) a partir do banco obtido
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            itemsCollection.DeleteOne(item => item.Id == id);
        }

        public Item GetItem(Guid id)
        {
            return itemsCollection.Find(item => item.Id == id).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            itemsCollection.ReplaceOne(existingItem => existingItem.Id == item.Id, item);
        }
    }
}