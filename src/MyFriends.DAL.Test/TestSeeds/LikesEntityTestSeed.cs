using MongoDB.Bson;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL.Test.TestSeeds
{
    public static class LikesEntityTestSeed
    {
        public static readonly LikesEntity likesEntity_LikeTest_GetAllByFriend1 = new LikesEntity()
        {
            Id = ObjectId.GenerateNewId(),
            FriendId = FriendEntityTestSeed.friendEntity_LikeTest.Id,
            LikeTypeId = LikeTypeEntityTestSeed.likeTypeEntity_LikeTest_1.Id
        };

        public static readonly LikesEntity likesEntity_LikeTest_GetAllByFriend2 = new LikesEntity()
        {
            Id = ObjectId.GenerateNewId(),
            FriendId = FriendEntityTestSeed.friendEntity_LikeTest.Id,
            LikeTypeId = LikeTypeEntityTestSeed.likeTypeEntity_LikeTest_2.Id
        };

        public static readonly LikesEntity likesEntity_LikeTest_Insert = new LikesEntity()
        {
            Id = ObjectId.GenerateNewId(),
            FriendId = FriendEntityTestSeed.friendEntity_LikeTest2.Id,
            LikeTypeId = LikeTypeEntityTestSeed.likeTypeEntity_LikeTest_1.Id
        };

        public static LikesEntity likesEntity_LikeTest_Delete = new LikesEntity()
        {
            Id = ObjectId.GenerateNewId(),
            FriendId = FriendEntityTestSeed.friendEntity_LikeTest2.Id,
            LikeTypeId = LikeTypeEntityTestSeed.likeTypeEntity_LikeTest_2.Id
        };

        public static void Seed(MongoDbContext db)
        {
            var collection = db.GetCollection<LikesEntity>();
            collection.InsertMany([
                likesEntity_LikeTest_GetAllByFriend1,
                likesEntity_LikeTest_GetAllByFriend2,
                likesEntity_LikeTest_Delete
                ]);
        }
    }
}
