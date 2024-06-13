using MongoDB.Bson;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL.Test.TestSeeds
{
    public static class LikeTypeEntityTestSeed
    {
        public static readonly LikeTypeEntity likeTypeEntity_LikeTest_1 = new LikeTypeEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Type = "Likes"
        };

        public static readonly LikeTypeEntity likeTypeEntity_LikeTest_2 = new LikeTypeEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Type = "Hates"
        };

        public static void Seed(MongoDbContext db)
        {
            var collection = db.GetCollection<LikeTypeEntity>();

            collection.InsertMany([
                likeTypeEntity_LikeTest_1,
                likeTypeEntity_LikeTest_2
                ]);
        }
    }
}
