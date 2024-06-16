using MongoDB.Bson;

namespace MyFriends.BL.Models
{
    public interface IModel
    {
        public ObjectId Id { get; set; }
    }
}
