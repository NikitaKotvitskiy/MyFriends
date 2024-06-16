using MongoDB.Bson;
using MyFriends.BL.Facades;
using MyFriends.BL.Mappers;
using MyFriends.BL.Models;
using MyFriends.DAL;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;

namespace MyFriends.BL.Test
{
    public class BLTests
    {
        private FriendFacade _friendFacade;
        private LikesFacade _likesFacade;
        private RelationFacade _relationFacade;

        [SetUp]
        public async Task Setup()
        {
            var context = new MongoDbContext("mongodb://localhost:27017", "MyFriendsTestDb");
            await context.DropDatabaseAsync();

            var friendRepo = new Repository<FriendEntity>(context);
            var likesRepo = new Repository<LikesEntity>(context);
            var likeTypeRepo = new Repository<LikeTypeEntity>(context);
            var relationRepo = new Repository<RelationEntity>(context);
            var relationTypeRepo = new Repository<RelationTypeEntity>(context);

            var friendMapper = new FriendMapper();
            var likeMapper = new LikeMapper();
            var relationMapper = new RelationMapper();

            _friendFacade = new FriendFacade(friendRepo, likesRepo, relationRepo, friendMapper);
            _likesFacade = new LikesFacade(likesRepo, likeTypeRepo, likeMapper);
            _relationFacade = new RelationFacade(relationRepo, relationTypeRepo, friendRepo, relationMapper);
        }

        [Test]
        public async Task Scenario1()
        {
            // Create two friends: A and B
            // A likes Apples and Oranges
            // B likes Apples and Lemons
            // Friend2 loves Friend1

            // Act
            // Adding friends
            var friendAId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "A" });
            var friendBId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "B" });

            // Adding likes for friend A
            await _likesFacade.InsertNewLikes(LikeListModel.Empty with
            {
                FriendId = friendAId,
                Type = "Apples"
            });
            await _likesFacade.InsertNewLikes(LikeListModel.Empty with
            {
                FriendId = friendAId,
                Type = "Oranges"
            });

            // Adding likes for friend B
            // Getting list of likes and chosing Apples
            var appleLikeTypeId = (await _likesFacade.GetAllLikeTypes()).Where(i => i.Type == "Apples").ToList()[0].Id;

            await _likesFacade.InsertNewLikes(LikeListModel.Empty with
            {
                FriendId = friendBId,
                LikeTypeId = appleLikeTypeId
            });
            await _likesFacade.InsertNewLikes(LikeListModel.Empty with
            {
                FriendId = friendBId,
                Type = "Lemons"
            });

            // Add relations
            // Getting list of friends and chosing friend A (we already have his Id, but it is a simulation of choosing)
            friendAId = (await _friendFacade.GetFriendsList()).Where(i => i.Name == "A").ToList()[0].Id;

            await _relationFacade.InsertNewRelation(RelationListModel.Empty with
            {
                FromFriendId = friendBId,
                ToFriendId = friendAId,
                Type = "loves"
            });

            // Assert
            // Check that we have our friends stored
            var friendsList = await _friendFacade.GetFriendsList();
            Assert.That(friendsList.Contains(FriendListModel.Empty with { Id = friendAId, Name = "A" }));
            Assert.That(friendsList.Contains(FriendListModel.Empty with { Id = friendBId, Name = "B" }));

            // Check that we have appls, oranges and lemons in like types
            var likeTypes = await _likesFacade.GetAllLikeTypes();
            Assert.That(likeTypes.Where(i => i.Type == "Apples").Count() == 1);
            Assert.That(likeTypes.Where(i => i.Type == "Oranges").Count() == 1);
            Assert.That(likeTypes.Where(i => i.Type == "Lemons").Count() == 1);

            // Check that A likes Apples and doesn't like Lemons
            var aLikes = await _likesFacade.GetAllLikes(friendAId);
            Assert.That(aLikes.Where(i => i.Type == "Apples").Count() == 1);
            Assert.That(aLikes.Where(i => i.Type == "Lemons").Count() == 0);

            // Check that B likes A, but A does not like B
            var aRelations = await _relationFacade.GetAllRelations(friendAId);
            Assert.That(aRelations.Count() == 0);
            var bRelations = await _relationFacade.GetAllRelations(friendBId);
            Assert.That(bRelations.Where(i => i.FriendName == "A").Count() == 1);
        }

        [Test]
        public async Task Scenario2()
        {
            // Create friend C
            // Create friend D
            // Say that C likes Bananas
            // Say that C hates D
            // Try to say that C likes Bananas again
            // Try to say that C hates D again

            // Act
            var friendCId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "C" });
            var friendDId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "D" });

            await _likesFacade.InsertNewLikes(LikeListModel.Empty with { FriendId = friendCId, Type = "Bananas" });
            var doubleLikeResult = await _likesFacade.InsertNewLikes(LikeListModel.Empty with { FriendId = friendCId, Type = "Bananas" });
            await _relationFacade.InsertNewRelation(RelationListModel.Empty with { FromFriendId = friendCId, ToFriendId = friendDId, Type = "hates" });
            var doubleRelationResult = await _relationFacade.InsertNewRelation(RelationListModel.Empty with { FromFriendId = friendCId, ToFriendId = friendDId, Type = "hates" });

            // Assert
            Assert.That(doubleLikeResult == null);
            Assert.That(doubleRelationResult == null);
        }

        [Test]
        public async Task Scenario3()
        {
            // Create friend E
            // Create friend F
            // Create friend G
            // Say that E ignores F
            // Say that F ignores G
            // Say thet G ignores E
            // Say that E likes chilli
            // Delete E
            // Check if related entities are deleted

            // Arrange && Act
            var friendEId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "E" });
            var friendFId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "F" });
            var friendGId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "G" });

            var eIgnoresFId = (ObjectId)await _relationFacade.InsertNewRelation(RelationListModel.Empty with
            {
                FromFriendId = friendEId,
                ToFriendId = friendFId,
                Type = "ignores"
            });
            var fIgnoresGId = (ObjectId)await _relationFacade.InsertNewRelation(RelationListModel.Empty with
            {
                FromFriendId = friendFId,
                ToFriendId = friendGId,
                Type = "ignores"
            });
            var gIgnoresEId = (ObjectId)await _relationFacade.InsertNewRelation(RelationListModel.Empty with
            {
                FromFriendId = friendGId,
                ToFriendId = friendEId,
                Type = "ignores"
            });

            var eLikesChilliId = (ObjectId)await _likesFacade.InsertNewLikes(LikeListModel.Empty with
            {
                FriendId = friendEId,
                Type = "Chilly"
            });

            await _friendFacade.DeleteFriend((await _friendFacade.GetFriend(friendEId))!);

            // Assert
            Assert.That(await _friendFacade.GetFriend(friendEId), Is.Null);
            Assert.That((await _likesFacade.GetAllLikes(friendEId)).Any() == false);
            Assert.That((await _relationFacade.GetAllRelations(friendEId)).Any() == false);
            Assert.That((await _relationFacade.GetAllRelations(friendGId)).Any() == false);
            Assert.That((await _relationFacade.GetAllRelationTypes()).Where(i => i.Type == "ignores").Count() == 1);
        }

        [Test]
        public async Task Scenario4()
        {
            // Create friend H
            // Create friend I
            // H blames I
            // I has changes his name to J
            // Check that H blames J now

            // Arrange && Act
            var friendHId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "H" });
            var friendIId = (ObjectId)await _friendFacade.InsertNewFriend(FriendDetailModel.Empty with { Name = "I" });
            await _relationFacade.InsertNewRelation(RelationListModel.Empty with
            {
                FromFriendId = friendHId,
                ToFriendId = friendIId,
                Type = "blames"
            });
            var friendJ = await _friendFacade.GetFriend(friendIId);
            friendJ!.Name = "J";
            await _friendFacade.UpdateFriend(friendJ);

            // Assert
            var hToI = (await _relationFacade.GetAllRelations(friendHId)).ToList()[0];
            var friendToBlame = await _friendFacade.GetFriend(hToI.ToFriendId);
            Assert.That(friendToBlame!.Name == "J");
        }
    }
}