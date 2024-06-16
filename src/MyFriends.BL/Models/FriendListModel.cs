using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record FriendListModel : IModel
    {
        public required ObjectId Id { get; set; }

        public required string Name { get; set; }
        public string? Surname { get; set; }

        public static FriendListModel Empty => new()
        {
            Id = default(ObjectId),
            Name = String.Empty
        };
    }
}
