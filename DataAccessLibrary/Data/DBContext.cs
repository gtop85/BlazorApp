using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class DBContext : IDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public DBContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            _mongoDatabase = client.GetDatabase(databaseSettings.DatabaseName);
        }

        public async Task InsertRecordAsync<T>(string table, T record)
        {
            var collection = _mongoDatabase.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
        }

        public async Task<PagedCollection<T>> GetRecordsAsync<T>(string table, int page, int pageSize)
        {
            var collection = _mongoDatabase.GetCollection<T>(table);

            var items = await collection.CountDocumentsAsync(new BsonDocument());

            int skip = (page - 1) * pageSize;

            var data = collection.Find(new BsonDocument()).Skip(skip).Limit(pageSize).ToList();
            
            var result = new PagedCollection<T>
            {
                Items = data,
                CurrentPage = page,
                TotalCount = (int)items,
                TotalPages = (int)Math.Ceiling((float)items / pageSize)
            };

            return result;
        }

        public async Task<bool> UpdateAsync<T>(string table, Guid id, T record)
        {
            var collection = _mongoDatabase.GetCollection<T>(table);
            var result = await collection.ReplaceOneAsync(
                new BsonDocument("_id", id.ToString()),
                record,
                new ReplaceOptions { IsUpsert = false });

            return result.ModifiedCount == 1;
        }

        public async Task<bool> DeleteRecordAsync<T>(string table, Guid id)
        {
            var collection = _mongoDatabase.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id.ToString());
            var result =  await collection.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
    }
}
