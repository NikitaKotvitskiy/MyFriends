using MongoDB.Bson;
using MyFriends.BL.Mappers;
using MyFriends.BL.Models;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;

namespace MyFriends.BL.Facades
{
    internal class RelationFacade(
        Repository<RelationEntity> relationRepo,
        Repository<RelationTypeEntity> relationTypeRepo,
        Repository<FriendEntity> friendRepo,
        RelationMapper mapper) : IFacade
    {
        public async Task<IEnumerable<RelationListModel>> GetAllRelations(ObjectId friendId)
        {
            var relations = (await relationRepo.GetAsync()).Where(i => i.FromFriendId == friendId);
            List<RelationListModel> relationListModels = new List<RelationListModel>();
            foreach (var relation in relations)
            {
                var type = await relationTypeRepo.GetByIdAsync(relation.RelationTypeId);
                var friend = await friendRepo.GetByIdAsync(relation.ToFriendId);
                relationListModels.Add(mapper.MapToListModel(relation, type, friend));
            }
            return relationListModels;
        }

        public async Task<bool> DeleteRelation(RelationListModel relation) =>
            await relationRepo.DeleteByIdAsync(relation.RelationTypeId);

        public async Task<ObjectId?> InsertNewRelation(RelationListModel relation)
        {
            var relationEntity = mapper.MatToRealtionEntity(relation);
            if (relationEntity.RelationTypeId == default(ObjectId))
            {
                var relationTypeEntity = mapper.MapToRelationTypeEntity(relation);
                relationTypeEntity.Id = ObjectId.GenerateNewId();
                relationEntity.RelationTypeId = relationTypeEntity.Id;
                await relationTypeRepo.InsertAsync(relationTypeEntity);
            }
            relation.RelationId = ObjectId.GenerateNewId();
            return await relationRepo.InsertAsync(relationEntity);
        }
    }
}
