using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Role>
    {
        public Task<Role?> GetByNameAsync(string name);
    }
}
