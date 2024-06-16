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
            // Get all likes entities
            var likes = (await likesRepo.GetAsync()).Where(i => i.FriendId == friendId);

            // Translate these entities to the list of Like List Models
            List<LikeListModel> likesListModels = new List<LikeListModel>();
            foreach (var like in likes)
            {
                var type = await likeTypeRepo.GetByIdAsync(like.LikeTypeId);
                likesListModels.Add(mapper.MapToLikeListModel(like, type));
            }
            return likesListModels;
        }

        public async Task<IEnumerable<LikeTypeListModel>> GetAllLikeTypes()
        {
            // Get all like types and translate it to Like Type List Models
            var types = await likeTypeRepo.GetAsync();
            List<LikeTypeListModel> likeTypeListModels = new List<LikeTypeListModel>();
            foreach (var type in types)
                likeTypeListModels.Add(mapper.MapToLikeTypeListModel(type));
            return likeTypeListModels;
        }

        public async Task<bool> DeleteLikes(LikeListModel likes) =>
            await likesRepo.DeleteByIdAsync(likes.LikesId);

        public async Task<ObjectId?> InsertNewLikes(LikeListModel likes)
        {
            // Translate the model to entity
            var likesEntity = mapper.MapToLikesEntity(likes);

            // If LikeTypeId in default, then user tries to create new like type (its name must be specified)
            if (likes.LikeTypeId == default(ObjectId))
            {
                // Try to get existed like type with the same name
                var existedLikeType = (await likeTypeRepo.GetAsync()).Where(i => i.Type == likes.Type);

                // If there is not like type with the same name
                if (existedLikeType.Count() == 0)
                {
                    // Translate model to like type entity
                    var likeTypeEntity = mapper.MapToLikeTypeEntity(likes);
                    // Generate new like type entity Id
                    likeTypeEntity.Id = ObjectId.GenerateNewId();
                    // Set this Id into likes entity
                    likesEntity.LikeTypeId = likeTypeEntity.Id;
                    // Add new like type entity to DB
                    await likeTypeRepo.InsertAsync(likeTypeEntity);
                }
                // In other case, reference existing one
                else
                    likesEntity.LikeTypeId = existedLikeType.First().Id;
            }

            // Check if the same like entity already exist
            if ((await likesRepo.GetAsync()).Where(i => i.FriendId == likesEntity.FriendId && i.LikeTypeId == likesEntity.LikeTypeId).Any())
                return null;

            // If it doesn't, generate new Id and insert the entity
            likes.LikesId = ObjectId.GenerateNewId();
            return await likesRepo.InsertAsync(likesEntity);
        }
    }
}
