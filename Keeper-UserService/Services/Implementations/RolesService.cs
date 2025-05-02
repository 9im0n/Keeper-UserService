using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
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

        public async Task<ServiceResponse<List<RoleDTO>>> GetAllRolesAsync()
        {
            List<Role> roles = await _rolesRepository.GetAllAsync();
            return ServiceResponse<List<RoleDTO>>.Success(
                roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).ToList()
            );
        }

        public async Task<ServiceResponse<RoleDTO?>> GetByIdAsync(Guid id)
        {
            Role? role = await _rolesRepository.GetByIdAsync(id);

            if (role == null)
                return ServiceResponse<RoleDTO?>.Fail(default, 404, "Role doesn't exist.");

            RoleDTO roleDTO = new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name,
            };

            return ServiceResponse<RoleDTO?>.Success(roleDTO);
        }

        private async Task<ServiceResponse<RoleDTO?>> GetByNameAsync(string name)
        {
            Role? role = await _rolesRepository.GetByNameAsync(name);

            if (role == null)
                return ServiceResponse<RoleDTO?>.Fail(default, 404, "Role doesn't exist.");

            RoleDTO roleDTO = new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name,
            };

            return ServiceResponse<RoleDTO?>.Success(roleDTO);
        }

        public async Task<ServiceResponse<RoleDTO?>> GetUserRoleAsync() => await GetByNameAsync("user");
        public async Task<ServiceResponse<RoleDTO?>> GetModerRoleAsync() => await GetByNameAsync("moder");
        public async Task<ServiceResponse<RoleDTO?>> GetAdminRoleAsync() => await GetByNameAsync("admin");
    }
}
