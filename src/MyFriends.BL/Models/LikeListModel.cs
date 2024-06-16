using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record LikeListModel : IModel
    {
        public required ObjectId Id { get; set; }
        public required string Type { get; set; }

        public static LikeListModel Empty => new()
        {
            Id = default(ObjectId),
            Type = String.Empty
        };
    }
}
