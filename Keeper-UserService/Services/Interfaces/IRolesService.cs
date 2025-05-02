using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IRolesService
    {
        public Task<ServiceResponse<List<RoleDTO>>> GetAllRolesAsync();
        public Task<ServiceResponse<RoleDTO?>> GetByIdAsync(Guid id);
        public Task<ServiceResponse<RoleDTO?>> GetUserRoleAsync();
        public Task<ServiceResponse<RoleDTO?>> GetModerRoleAsync();
        public Task<ServiceResponse<RoleDTO?>> GetAdminRoleAsync();
    }
}
