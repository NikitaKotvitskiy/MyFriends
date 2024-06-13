using MongoDB.Bson;

namespace MyFriends.DAL.Entities
{
    public record LikesEntity : IEntity
    {
        public required ObjectId Id { get; set; }
        public required ObjectId FriendId { get; set; }
        public required ObjectId LikeTypeId { get; set; }
    }
}
