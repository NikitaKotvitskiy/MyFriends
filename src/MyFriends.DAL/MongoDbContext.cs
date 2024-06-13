using MongoDB.Driver;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL
{
    internal class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<FriendEntity> Friends => _database.GetCollection<FriendEntity>("Friends");
        public IMongoCollection<LikesEntity> Likes => _database.GetCollection<LikesEntity>("Likes");
        public IMongoCollection<LikeTypeEntity> LikeTypes => _database.GetCollection<LikeTypeEntity>("LikeTypes");
        public IMongoCollection<RelationEntity> Relations => _database.GetCollection<RelationEntity>("Relations");
        public IMongoCollection<RelationTypeEntity> RealtionTypes => _database.GetCollection<RelationTypeEntity>("RelationTypes");
    }
}
