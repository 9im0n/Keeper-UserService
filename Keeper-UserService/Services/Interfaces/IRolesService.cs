using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IRolesService
    {
        public Task<Roles?> GetByIdAsync(Guid id);
        public Task<Roles?> GetByNameAsync(string name);
        public Task<Roles> CreateAsync(Roles role);
        public Task<Roles> UpdateAsync(Roles role);
        public Task<Roles> DeleteAsync(Guid id);
    }
}
