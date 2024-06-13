using MongoDB.Bson;

namespace MyFriends.DAL.Entities
{
    internal interface IEntity
    {
        public ObjectId Id { get; set; }
    }
}
