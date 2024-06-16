using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record RelationListModel : IModel
    {
        public required ObjectId Id { get; set; }
        public required string Type { get; set; }
        public required ObjectId FriendId { get; set; }
        public required string FriendName { get; set; }
        public string? FriendSurname { get; set; }

        public static RelationListModel Empty => new()
        {
            Id = default(ObjectId),
            Type = String.Empty,
            FriendId = default(ObjectId),
            FriendName = String.Empty
        };
    }
}
