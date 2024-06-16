using MyFriends.BL.Models;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Mappers;

namespace MyFriends.BL.Mappers
{
    public class LikeMapper : IMapper
    {
        public LikeListModel MapToLikeListModel(LikesEntity likedEntity, LikeTypeEntity typeEntity) =>
            LikeListModel.Empty with
            {
                LikesId = likedEntity.Id,
                FriendId = likedEntity.FriendId,
                LikeTypeId = typeEntity.Id,
                Type = typeEntity.Type
            };

        public LikeTypeListModel MapToLikeTypeListModel(LikeTypeEntity typeEntity) =>
            LikeTypeListModel.Empty with
            {
                Id = typeEntity.Id,
                Type = typeEntity.Type
            };

        public LikeTypeEntity MapToLikeTypeEntity(LikeListModel model) =>
            new()
            {
                Id = model.LikeTypeId,
                Type = model.Type
            };

        public LikesEntity MapToLikesEntity(LikeListModel model) =>
            new()
            {
                Id = model.LikesId,
                FriendId = model.FriendId,
                LikeTypeId = model.LikeTypeId
            };
    }
}
