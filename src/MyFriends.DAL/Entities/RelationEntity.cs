namespace MyFriends.DAL.Entities
{
    public record RelationEntity : IEntity
    {
        public required Guid Id { get; set; }
        public required Guid FromFriendId { get; set; }
        public required Guid ToFriendId { get; set; }
    }
}
