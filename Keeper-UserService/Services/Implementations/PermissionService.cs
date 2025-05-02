using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implementations
{
    public class PermissionService : IPermissionsService
    {
        private readonly IBaseRepository<Permission> _repository;

        public PermissionService(IBaseRepository<Permission> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<List<PermissionDTO>>> GetAllPermissionsAsync()
        {
            List<PermissionDTO> permissions = (await _repository.GetAllAsync()).Select(p =>
            new PermissionDTO() { Id = p.Id, Name = p.Name }).ToList();

            return ServiceResponse<List<PermissionDTO>>.Success(permissions);
        }

        public async Task<ServiceResponse<PermissionDTO?>> GetByIdAsync(Guid id)
        {
            Permission? permission = await _repository.GetByIdAsync(id);

            if (permission == null)
                return ServiceResponse<PermissionDTO?>.Fail(default, 404, "Permission doesn't exist.");

            PermissionDTO permissionDTO = new PermissionDTO()
            {
                Id = permission.Id,
                Name = permission.Name,
            };

            return ServiceResponse<PermissionDTO?>.Success(permissionDTO);
        }
    }
}
