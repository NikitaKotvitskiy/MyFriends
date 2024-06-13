using MongoDB.Bson;

namespace MyFriends.DAL.Entities
{
    public record RelationEntity : IEntity
    {
        public required ObjectId Id { get; set; }
        public required ObjectId FromFriendId { get; set; }
        public required ObjectId ToFriendId { get; set; }
    }
}
