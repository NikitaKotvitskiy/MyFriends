using MyFriends.BL.Models;
using MyFriends.DAL.Entities;

namespace MyFriends.BL.Mappers
{
    public class RelationMapper
    {
        public RelationListModel MapToListModel(RelationEntity relationEntity, RelationTypeEntity relationType, FriendEntity toFriendEntity) =>
            RelationListModel.Empty with
            {
                RelationId = relationEntity.Id,
                FromFriendId = relationEntity.FromFriendId,
                ToFriendId = relationEntity.ToFriendId,
                FriendName = toFriendEntity.Name,
                FriendSurname = toFriendEntity.Surname,
                Type = relationType.Type
            };

        public RelationEntity MatToRealtionEntity(RelationListModel model) =>
            new()
            {
                Id = model.RelationId,
                FromFriendId = model.FromFriendId,
                ToFriendId = model.ToFriendId,
                RelationTypeId = model.RelationTypeId
            };

        public RelationTypeEntity MapToRelationTypeEntity(RelationListModel model) =>
            new()
            {
                Id = model.RelationTypeId,
                Type = model.Type
            };
    }
}
