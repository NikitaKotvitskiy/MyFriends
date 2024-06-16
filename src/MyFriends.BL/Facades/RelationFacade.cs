using MongoDB.Bson;
using MyFriends.BL.Mappers;
using MyFriends.BL.Models;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Repositories;

namespace MyFriends.BL.Facades
{
    public class RelationFacade(
        Repository<RelationEntity> relationRepo,
        Repository<RelationTypeEntity> relationTypeRepo,
        Repository<FriendEntity> friendRepo,
        RelationMapper mapper) : IFacade
    {
        public async Task<IEnumerable<RelationListModel>> GetAllRelations(ObjectId friendId)
        {
            // Get all relations with specified friend Id
            var relations = (await relationRepo.GetAsync()).Where(i => i.FromFriendId == friendId);

            // Translate these entities to Relation List Models
            List<RelationListModel> relationListModels = new List<RelationListModel>();
            foreach (var relation in relations)
            {
                var type = await relationTypeRepo.GetByIdAsync(relation.RelationTypeId);
                var friend = await friendRepo.GetByIdAsync(relation.ToFriendId);
                relationListModels.Add(mapper.MapToRelationListModel(relation, type, friend));
            }
            return relationListModels;
        }

        public async Task<IEnumerable<RelationTypeListModel>> GetAllRelationTypes()
        {
            var types = await relationTypeRepo.GetAsync();
            List<RelationTypeListModel> relationTypeListModels = new List<RelationTypeListModel>();
            foreach (var type in types)
                relationTypeListModels.Add(mapper.MapToRelationTypeListModel(type));
            return relationTypeListModels;
        }

        public async Task<bool> DeleteRelation(RelationListModel relation) =>
            await relationRepo.DeleteByIdAsync(relation.RelationTypeId);

        public async Task<ObjectId?> InsertNewRelation(RelationListModel relation)
        {
            // Map the model to relation entity
            var relationEntity = mapper.MatToRealtionEntity(relation);
            // If relation type Id is not specified
            if (relationEntity.RelationTypeId == default(ObjectId))
            {
                // Try to find exisiting relation type with the same name
                var existedRelationType = (await relationTypeRepo.GetAsync()).Where(i => i.Type == relation.Type);
                // If it does not exist
                if (existedRelationType.Count() == 0)
                {
                    // Create new Relation type entity and insert it
                    var relationTypeEntity = mapper.MapToRelationTypeEntity(relation);
                    relationTypeEntity.Id = ObjectId.GenerateNewId();
                    relationEntity.RelationTypeId = relationTypeEntity.Id;
                    await relationTypeRepo.InsertAsync(relationTypeEntity);
                }
                // In other case, reference existing one for avoiding duplicated
                else
                    relationEntity.RelationTypeId = existedRelationType.First().Id;
            }

            // Check if this relation is not already in DB
            if ((await relationRepo.GetAsync()).Where(
                i => i.FromFriendId == relationEntity.FromFriendId 
                && i.ToFriendId == relationEntity.ToFriendId 
                && i.RelationTypeId == relationEntity.RelationTypeId).Any())
                return null;

            // If not, generate Id and insert
            relation.RelationId = ObjectId.GenerateNewId();
            return await relationRepo.InsertAsync(relationEntity);
        }
    }
}
