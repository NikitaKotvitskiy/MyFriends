using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record RelationTypeListModel
    {
        public required ObjectId Id { get; set; }
        public required string Type { get; set; }

        public static RelationTypeListModel Empty => new()
        {
            Id = default,
            Type = String.Empty,
        };
    }
}
