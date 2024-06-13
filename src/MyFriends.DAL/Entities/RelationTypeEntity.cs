using MongoDB.Bson;

namespace MyFriends.DAL.Entities
{
    public record RelationTypeEntity : IEntity
    {
        public required ObjectId Id { get; set; }
        public required string Type { get; set; }
    }
}
