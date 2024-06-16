using MongoDB.Bson;
using MyFriends.BL.Mappers;
using MyFriends.BL.Models;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;

namespace MyFriends.BL.Facades
{
    public class LikesFacade(
        Repository<LikesEntity> likesRepo,
        Repository<LikeTypeEntity> likeTypeRepo,
        LikeMapper mapper) : IFacade 
    {
        public async Task<IEnumerable<LikeListModel>> GetAllLikes(ObjectId friendId)
        {
            var likes = (await likesRepo.GetAsync()).Where(i => i.FriendId == friendId);
            List<LikeListModel> likesListModels = new List<LikeListModel>();
            foreach (var like in likes)
            {
                var type = await likeTypeRepo.GetByIdAsync(like.LikeTypeId);
                likesListModels.Add(mapper.MapToLikeListModel(like, type));
            }
            return likesListModels;
        }

        public async Task<bool> DeleteLikes(LikeListModel likes) =>
            await likesRepo.DeleteByIdAsync(likes.LikesId);

        public async Task<ObjectId?> InsertNewLikes(LikeListModel likes)
        {
            var likesEntity = mapper.MapToLikesEntity(likes);
            if (likes.LikeTypeId == default(ObjectId))
            {
                var likeTypeEntity = mapper.MapToLikeTypeEntity(likes);
                likeTypeEntity.Id = ObjectId.GenerateNewId();
                likesEntity.LikeTypeId = likeTypeEntity.Id;
                await likeTypeRepo.InsertAsync(likeTypeEntity);
            }
            likes.LikesId = ObjectId.GenerateNewId();
            return await likesRepo.InsertAsync(likesEntity);
        }
    }
}
