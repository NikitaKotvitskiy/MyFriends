using MongoDB.Bson;
using MyFriends.BL.Facades;
using MyFriends.BL.Mappers;
using MyFriends.BL.Models;
using MyFriends.DAL;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;

namespace MyFriends.BL
{
    public static class BLFactory
    {
        public static FacadeSet GetFacades(string connectionString, string dbName)
        {
            var context = new MongoDbContext(connectionString, dbName);

            var friendsRepo = new Repository<FriendEntity>(context);
            var likeTypeRepo = new Repository<LikeTypeEntity>(context);
            var relationTypeRepo = new Repository<RelationTypeEntity>(context);
            var likesRepo = new Repository<LikesEntity>(context);
            var relationRepo = new Repository<RelationEntity>(context);

            var friendMapper = new FriendMapper();
            var likeMapper = new LikeMapper();
            var relationMapper = new RelationMapper();

            var friendFacade = new FriendFacade(friendsRepo, likesRepo, relationRepo, friendMapper);
            var likesFacade = new LikesFacade(likesRepo, likeTypeRepo, likeMapper);
            var relationFacade = new RelationFacade(relationRepo, relationTypeRepo, friendsRepo, relationMapper);

            return new FacadeSet(friendFacade, likesFacade, relationFacade);
        }
    }
}
