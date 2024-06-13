using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;
using MyFriends.DAL.Test.TestSeeds;

namespace MyFriends.DAL.Test
{
    public class LikesTests
    {
        private MongoDbContext _db;

        [SetUp]
        public async Task Setup()
        {
            _db = await TestData.GetTestContext();
        }

        [Test]
        public async Task LikesTest_GetAllByFriend()
        {
            // Arrange
            var repo = new Repository<LikesEntity>(_db);
            var friendId = FriendEntityTestSeed.friendEntity_LikeTest.Id;

            // Act
            var likesList = (await repo.GetAsync()).Where(i => i.FriendId == friendId);

            // Assert
            Assert.That(likesList.Count(), Is.EqualTo(2));
            Assert.That(likesList.ToArray(), Does.Contain(LikesEntityTestSeed.likesEntity_LikeTest_GetAllByFriend1));
            Assert.That(likesList.ToArray(), Does.Contain(LikesEntityTestSeed.likesEntity_LikeTest_GetAllByFriend2));
        }

        [Test]
        public async Task LikesTest_Insert()
        {
            // Arrange
            var repo = new Repository<LikesEntity>(_db);
            var insertEntity = LikesEntityTestSeed.likesEntity_LikeTest_Insert;

            // Act
            await repo.InsertAsync(insertEntity);
            var likesList = (await repo.GetAsync()).Where(i => i.FriendId == insertEntity.FriendId);

            // Assert
            Assert.That(likesList.ToArray(), Does.Contain(insertEntity));
        }

        [Test]
        public async Task LikesTest_Delete()
        {
            // Arrange
            var repo = new Repository<LikesEntity>(_db);
            var deleted = LikesEntityTestSeed.likesEntity_LikeTest_Delete;

            // Act
            await repo.DeleteByIdAsync(deleted.Id);
            var likesList = (await repo.GetAsync()).Where(i => i.FriendId == deleted.FriendId);

            // Assert
            Assert.That(likesList, !Does.Contain(deleted));
        }
    }
}
