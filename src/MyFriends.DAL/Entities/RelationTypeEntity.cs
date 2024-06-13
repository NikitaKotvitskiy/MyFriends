namespace MyFriends.DAL.Entities
{
    public record RelationTypeEntity : IEntity
    {
        public required Guid Id { get; set; }
        public required string Type { get; set; }
    }
}
