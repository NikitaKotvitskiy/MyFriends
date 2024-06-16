using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record RelationListModel : IModel
    {
        public required ObjectId RelationId { get; set; }
        public required ObjectId RelationTypeId { get; set; }
        public required ObjectId FromFriendId { get; set; }
        public required ObjectId ToFriendId { get; set; }

        public required string Type { get; set; }
        public required string FriendName { get; set; }
        public string? FriendSurname { get; set; }

        public static RelationListModel Empty => new()
        {
            RelationId = default(ObjectId),
            RelationTypeId = default(ObjectId),
            FromFriendId = default(ObjectId),
            ToFriendId = default(ObjectId),
            Type = String.Empty,
            FriendName = String.Empty
        };
    }
}
