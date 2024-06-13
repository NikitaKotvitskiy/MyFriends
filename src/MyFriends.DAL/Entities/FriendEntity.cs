using MongoDB.Bson;

namespace MyFriends.DAL.Entities
{
    public record FriendEntity : IEntity
    {
        public required ObjectId Id { get; set; }

        public required string Name { get; set; }
        public string Surname { get; set; } = String.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Country { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
    }
}
