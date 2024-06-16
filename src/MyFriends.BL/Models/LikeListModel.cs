using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record LikeListModel : IModel
    {
        public required ObjectId LikesId { get; set; }
        public required ObjectId FriendId { get; set; }
        public required ObjectId LikeTypeId { get; set; }

        public required string Type { get; set; }

        public static LikeListModel Empty => new()
        {
            LikesId = default(ObjectId),
            FriendId = default(ObjectId),
            LikeTypeId = default(ObjectId),
            Type = String.Empty
        };
    }
}
