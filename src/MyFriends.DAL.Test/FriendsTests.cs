using MongoDB.Bson;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;
using MyFriends.DAL.Test.TestSeeds;

namespace MyFriends.DAL.Test
{
    public class Tests
    {
        private MongoDbContext _db;

        [SetUp]
        public async Task Setup()
        {
            _db = await TestData.GetTestContext();
        }

        [Test]
        public async Task FriendsTest_GetByIdTest()
        {
            // Arrange
            var repo = new Repository<FriendEntity>(_db);
            var actualEntity = FriendEntityTestSeed.friendEntity_FriendTest_GetById;

            // Act
            var entity = await repo.GetByIdAsync(actualEntity.Id);

            // Assert
            Assert.That(entity, Is.EqualTo(actualEntity));
        }

        [Test]
        public async Task FriendsTest_GetAll()
        {
            // Arrange
            var repo = new Repository<FriendEntity>(_db);
            var entity = FriendEntityTestSeed.friendEntity_FriendTest_GetAll;

            // Act
            var allEntities = await repo.GetAsync();

            // Assert
            Assert.Contains(entity, allEntities);
        }

        [Test]
        public async Task FriendTest_Insert()
        {
            // Arrange
            var repo = new Repository<FriendEntity>(_db);
            var newEntity = new FriendEntity()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "NewName"
            };

            // Act
            await repo.InsertAsync(newEntity);
            var insertedEntity = await repo.GetByIdAsync(newEntity.Id);

            // Assert
            Assert.That(insertedEntity, Is.EqualTo(newEntity));
        }

        [Test]
        public async Task FriendTest_Delete()
        {
            // Arrange
            var repo = new Repository<FriendEntity>(_db);
            var deleteId = FriendEntityTestSeed.friendEntity_FriendTest_Delete.Id;

            // Act
            await repo.DeleteByIdAsync(deleteId);
            var deletedEntity = await repo.GetByIdAsync(deleteId);

            // Assert
            Assert.IsNull(deletedEntity);
        }

        [Test]
        public async Task FriendTest_Update()
        {
            // Arange
            var repo = new Repository<FriendEntity>(_db);
            var updateId = FriendEntityTestSeed.friendEntity_FriendTest_Update.Id;
            var updatedName = "Updated Name";
            
            // Act
            var entity = await repo.GetByIdAsync(updateId);
            entity.Name = updatedName;
            await repo.UpdateAsync(entity);
            var updatedEntity = await repo.GetByIdAsync(updateId);

            // Assert
            Assert.That(entity.Name, Is.EqualTo(updatedName));
        }
    }
}