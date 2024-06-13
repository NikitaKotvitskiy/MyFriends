using MongoDB.Bson;
using MyFriends.DAL.Entities;

namespace MyFriends.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        public Task InsertAsync(TEntity entity);
        public Task<List<TEntity>> GetAsync();
        public Task<TEntity> GetByIdAsync(ObjectId id);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteByIdAsync(ObjectId id);
    }
}
