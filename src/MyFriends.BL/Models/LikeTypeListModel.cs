using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public record LikeTypeListModel : IModel
    {
        public required ObjectId Id { get; set; }
        public required string Type;

        public static LikeTypeListModel Empty => new()
        {
            Id = default,
            Type = String.Empty
        };
    }
}
