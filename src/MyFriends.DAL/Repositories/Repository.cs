using MongoDB.Bson;
using MongoDB.Driver;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL.Repositories
{
    public class Repository<TEntity>(MongoDbContext context) : IRepository<TEntity> where TEntity : class, IEntity
    {
        private IMongoCollection<TEntity> collection = context.GetCollection<TEntity>();

        public async Task<bool> DeleteByIdAsync(ObjectId id)
        {
            var result = await collection.DeleteOneAsync(i => i.Id == id);
            if (result.DeletedCount == 0)
                return false;
            return true;
        }

        public async Task<List<TEntity>> GetAsync() =>
            await collection.Find(i => true).ToListAsync();

        public async Task<TEntity> GetByIdAsync(ObjectId id) =>
          await collection.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task<ObjectId?> InsertAsync(TEntity entity)
        {
            try
            {
                await collection.InsertOneAsync(entity);
                return entity.Id;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var result = await collection.ReplaceOneAsync(i => i.Id == entity.Id, entity);
            if (result.ModifiedCount == 0) 
                return false;
            return true;
        }
    }
}
