using MongoDB.Bson;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriends.DAL.Test.TestSeeds
{
    public static class FriendEntityTestSeed
    {
        public static readonly FriendEntity friendEntity_FriendTest_GetById = new FriendEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Name1",
            Surname = "Surname1",
            DateOfBirth = new DateOnly(2000, 1, 1),
            Country = "Country1",
            City = "City1"
        };

        public static readonly FriendEntity friendEntity_FriendTest_GetAll = new FriendEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Name2"
        };

        public static readonly FriendEntity friendEntity_FriendTest_Update = new FriendEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Name3"
        };

        public static readonly FriendEntity friendEntity_FriendTest_Delete = new FriendEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Name4"
        };

        public static readonly FriendEntity friendEntity_RelationTest = new FriendEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Name5"
        };

        public static readonly FriendEntity friendEntity_LikeTest = new FriendEntity()
        {
            Id = ObjectId.GenerateNewId(),
            Name = "Name6"
        };

        public static void Seed(MongoDbContext db)
        {
            var collection = db.GetCollection<FriendEntity>();
            collection.InsertMany([
                friendEntity_FriendTest_Delete,
                friendEntity_FriendTest_Update,
                friendEntity_FriendTest_GetAll,
                friendEntity_FriendTest_GetById,
                friendEntity_RelationTest,
                friendEntity_LikeTest
                ]);
        }
    }
}
