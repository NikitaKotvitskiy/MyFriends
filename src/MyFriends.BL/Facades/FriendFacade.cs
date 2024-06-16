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
            var friends = await repo.GetAsync();
            var friendListModels = new List<FriendListModel>();
            foreach (var friend in friends)
                friendListModels.Add(mapper.MapToFriendListModel(friend));

            return friendListModels;
        }

        public async Task<FriendDetailModel?> GetFriend(ObjectId id)
        {
            var friend = await repo.GetByIdAsync(id);
            if (friend == null)
                return null;
            return mapper.MapToFriendDetailModel(friend);
        }

        public async Task<bool> UpdateFriend(FriendDetailModel model)
        {
            var entity = mapper.MapToFriendEntity(model);
            return await repo.UpdateAsync(entity);
        }

        public async Task<ObjectId?> InsertNewFriend(FriendDetailModel model)
        {
            var entity = mapper.MapToFriendEntity(model);
            entity.Id = ObjectId.GenerateNewId();
            return await repo.InsertAsync(entity);
        }

        public async Task<bool> DeleteFriend(FriendDetailModel model)
        {
            if (await repo.DeleteByIdAsync(model.Id))
                return false;

            var likes = (await likesRepo.GetAsync()).Where(i => i.FriendId == model.Id);
            foreach (var likesEntity in likes)
                await likesRepo.DeleteByIdAsync(likesEntity.Id);

            var relations = (await relationRepo.GetAsync()).Where(i => i.FromFriendId == model.Id || i.ToFriendId == model.Id);
            foreach (var relationEntity in relations)
                await relationRepo.DeleteByIdAsync(relationEntity.Id);

            return true;
        }
    }
}
