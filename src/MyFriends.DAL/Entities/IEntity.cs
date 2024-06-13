using MongoDB.Bson;

namespace MyFriends.DAL.Entities
{
    public interface IEntity
    {
        public ObjectId Id { get; set; }
    }
}
