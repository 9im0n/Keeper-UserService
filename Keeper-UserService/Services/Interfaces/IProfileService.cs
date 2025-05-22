using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using System.Security.Claims;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<ServiceResponse<PagedResultDTO<ProfileDTO>>> GetProfilesPagedAsync(PagedRequestDTO<ProfileFilterDTO> pagedRequestDTO);
        public Task<ServiceResponse<ProfileDTO?>> GetByIdAsync(Guid id);
        public Task<ServiceResponse<ICollection<ProfileDTO>?>> GetBatchedAsync(BatchedProfilesQueryDTO request);
        public Task<ServiceResponse<ProfileDTO?>> CreateAsync(CreateProfileDTO createProfileDTO);
        public Task<ServiceResponse<ProfileDTO?>> UpdateAsync(Guid profileId, Guid userId, UpdateProfileDTO updateProfileDTO);
        public Task<ServiceResponse<ProfileDTO?>> UploadAvatarAsync(UploadAvatarDTO uploadAvatarDTO, Guid id, ClaimsPrincipal User);
    }
}
