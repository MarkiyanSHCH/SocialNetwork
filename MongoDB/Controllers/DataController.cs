using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.Controllers
{
    public class DataController
    {
        private IMongoDatabase db;

        public DataController(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertEntity<T>(string table, T entity)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(entity);
        }
        public List<T> ReadEntity<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T GetEntityById<T>(string table, ObjectId id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);
            return collection.Find(filter).First();
        }

        public void UpsertEntity<T>(string table, ObjectId id, T entity)
        {
            var collection = db.GetCollection<T>(table);

            var relust = collection.ReplaceOne(
                new BsonDocument("_id", id),
                entity,
                new UpdateOptions { IsUpsert = true });
        }

        public void DeleteEntity<T>(string table, ObjectId id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }
    }
}
