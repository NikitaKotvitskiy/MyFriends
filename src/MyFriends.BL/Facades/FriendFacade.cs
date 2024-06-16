using MongoDB.Bson;
using MyFriends.BL.Mappers;
using MyFriends.BL.Models;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;

namespace MyFriends.BL.Facades
{
    public class FriendFacade(
        Repository<FriendEntity> repo,
        Repository<LikesEntity> likesRepo,
        Repository<RelationEntity> relationRepo,
        FriendMapper mapper) : IFacade
    {
        public async Task<IEnumerable<FriendListModel>> GetFriendsList()
        {
            // Get all friends entities
            var friends = await repo.GetAsync();

            // Tranfsorm friends entities into list of friend list models
            var friendListModels = new List<FriendListModel>();
            foreach (var friend in friends)
                friendListModels.Add(mapper.MapToFriendListModel(friend));

            return friendListModels;
        }

        public async Task<FriendDetailModel?> GetFriend(ObjectId id)
        {
            // Get friend entity with specified Id
            var friend = await repo.GetByIdAsync(id);

            // If null, then this entity does not exist, or some internal error has happened
            if (friend == null)
                return null;

            // Return the entity mapped to friend detail model
            return mapper.MapToFriendDetailModel(friend);
        }

        public async Task<bool> UpdateFriend(FriendDetailModel model)
        {
            // Translate friend detail model to entity
            var entity = mapper.MapToFriendEntity(model);

            // Apply updating and return result
            return await repo.UpdateAsync(entity);
        }

        public async Task<ObjectId?> InsertNewFriend(FriendDetailModel model)
        {
            // Translate friend detail model to entity
            var entity = mapper.MapToFriendEntity(model);

            // Generate new ObjectId for new entity
            entity.Id = ObjectId.GenerateNewId();

            // Insert entity and return its object Id (or null, of insertion failed)
            return await repo.InsertAsync(entity);
        }

        public async Task<bool> DeleteFriend(FriendDetailModel model)
        {
            // Try to delete entity with If from friend detail model
            if (await repo.DeleteByIdAsync(model.Id) == false)
                return false;

            // Delelte all related likes
            var likes = (await likesRepo.GetAsync()).Where(i => i.FriendId == model.Id);
            foreach (var likesEntity in likes)
                await likesRepo.DeleteByIdAsync(likesEntity.Id);

            // Delete all related relations
            var relations = (await relationRepo.GetAsync()).Where(i => i.FromFriendId == model.Id || i.ToFriendId == model.Id);
            foreach (var relationEntity in relations)
                await relationRepo.DeleteByIdAsync(relationEntity.Id);

            return true;
        }
    }
}
