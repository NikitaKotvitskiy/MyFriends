using MongoDB.Bson;

namespace MyFriends.DAL.Entities
{
    public record FriendEntity : IEntity
    {
        public required ObjectId Id { get; set; }

        public required string Name { get; set; }
        public string? Surname { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
    }
}
