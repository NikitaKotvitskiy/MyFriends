namespace MyFriends.DAL.Entities
{
    public record LikesEntity : IEntity
    {
        public required Guid Id { get; set; }
        public required Guid FriendId { get; set; }
        public required Guid LikeTypeId { get; set; }
    }
}
