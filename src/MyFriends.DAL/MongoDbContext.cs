using MongoDB.Driver;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>() where T : IEntity => _database.GetCollection<T>(typeof(T).Name);
    }
}
