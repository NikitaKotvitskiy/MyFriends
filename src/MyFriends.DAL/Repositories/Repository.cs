using MongoDB.Bson;
using MongoDB.Driver;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL.Repositories
{
    public class Repository<TEntity>(MongoDbContext context) : IRepository<TEntity> where TEntity : class, IEntity
    {
        private IMongoCollection<TEntity> collection = context.GetCollection<TEntity>();

        public async Task DeleteByIdAsync(ObjectId id) =>
            await collection.DeleteOneAsync(i => i.Id == id);

        public async Task<List<TEntity>> GetAsync() =>
            await collection.Find(i => true).ToListAsync();

        public async Task<TEntity> GetByIdAsync(ObjectId id) =>
            await collection.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task InsertAsync(TEntity entity) =>
            await collection.InsertOneAsync(entity);

        public async Task UpdateAsync(TEntity entity) =>
            await collection.ReplaceOneAsync(i => i.Id == entity.Id, entity);
    }
}
