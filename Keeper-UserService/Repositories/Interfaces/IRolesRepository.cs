using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Roles>
    {
        public Task<Roles?> GetByNameAsync(string name);
    }
}
