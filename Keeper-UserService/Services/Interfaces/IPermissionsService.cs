using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IPermissionsService
    {
        public Task<ServiceResponse<List<PermissionDTO>>> GetAllPermissionsAsync();
        public Task<ServiceResponse<PermissionDTO?>> GetByIdAsync(Guid id);
    }
}
