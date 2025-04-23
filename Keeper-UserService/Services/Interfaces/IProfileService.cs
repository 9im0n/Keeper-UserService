using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<ServiceResponse<Profiles?>> GetByIdAsync(Guid id);
        public Task<ServiceResponse<Profiles?>> GetByUserIdAsync(Guid id);
        public Task<ServiceResponse<Profiles?>> CreateAsync(CreateProfileDTO createProfileDTO);
        public Task<ServiceResponse<Profiles?>> UpdateAsync(Guid id, UpdateProfileDTO updateProfileDTO);
        public Task<ServiceResponse<Profiles?>> DeleteAsync(Guid id);
    }
}
