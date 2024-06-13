using MongoDB.Driver;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL
{
    public class MongoDbContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public async Task DropDatabaseAsync() =>
            await _client.DropDatabaseAsync(_database.DatabaseNamespace.DatabaseName);

        public IMongoCollection<T> GetCollection<T>() where T : IEntity => _database.GetCollection<T>(typeof(T).Name);
    }
}
