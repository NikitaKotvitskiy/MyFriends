using MongoDB.Bson;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        public Task<ObjectId?> InsertAsync(TEntity entity);
        public Task<List<TEntity>> GetAsync();
        public Task<TEntity> GetByIdAsync(ObjectId id);
        public Task<bool> UpdateAsync(TEntity entity);
        public Task<bool> DeleteByIdAsync(ObjectId id);
    }
}
