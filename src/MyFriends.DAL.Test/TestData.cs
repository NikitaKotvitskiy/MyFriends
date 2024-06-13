using MongoDB.Bson;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;
using MyFriends.DAL.Test.TestSeeds;
using System;
using System.Collections.Generic;
using System.Globalization;
namespace MyFriends.DAL.Test
{
    public static class TestData
    {
        public static async Task<MongoDbContext> GetTestContext()
        {
            var context = new MongoDbContext("mongodb://localhost:27017", "MyFriendsTestDb");
            await context.DropDatabaseAsync();
            FriendEntityTestSeed.Seed(context);

            return context;
        }
    }
}
