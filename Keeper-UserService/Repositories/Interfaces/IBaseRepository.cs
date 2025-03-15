using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid id);
        public Task<T> CreateAsync(T obj);
        public Task<T?> UpdateAsync(T obj);
        public Task<T?> DeleteAsync(Guid id);
    }
}
