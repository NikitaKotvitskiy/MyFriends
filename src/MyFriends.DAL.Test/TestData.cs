using MyFriends.DAL.Test.TestSeeds;

namespace MyFriends.DAL.Test
{
    public static class TestData
    {
        public static async Task<MongoDbContext> GetTestContext()
        {
            var context = new MongoDbContext("mongodb://localhost:27017", "MyFriendsTestDb");
            await context.DropDatabaseAsync();
            FriendEntityTestSeed.Seed(context);
            LikeTypeEntityTestSeed.Seed(context);
            LikesEntityTestSeed.Seed(context);

            return context;
        }
    }
}
