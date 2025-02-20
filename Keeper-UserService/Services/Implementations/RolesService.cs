using Keeper_UserService.Models.Db;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;


        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }


        public async Task<Roles?> GetByIdAsync(Guid id)
        {
            return await _rolesRepository.GetByIdAsync(id);
        }


        public Task<Roles?> GetByNameAsync(string name)
        {
            return _rolesRepository.GetByNameAsync(name);
        }


        public async Task<Roles> CreateAsync(Roles role)
        {
            return await _rolesRepository.CreateAsync(role);
        }


        public async Task<Roles> UpdateAsync(Roles role)
        {
            return await _rolesRepository.UpdateAsync(role);
        }


        public async Task<Roles> DeleteAsync(Guid id)
        {
            return await _rolesRepository.DeleteAsync(id);
        }
    }
}
