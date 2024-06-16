using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record FriendDetailModel : IModel
    {
        public required ObjectId Id { get; set; }

        public required string Name { get; set; }
        public string? Surname { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }

        public static FriendDetailModel Empty => new()
        {
            Id = default(ObjectId),
            Name = string.Empty
        };
    }
}
