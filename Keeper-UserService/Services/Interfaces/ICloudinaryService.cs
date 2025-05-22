using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Services.Interfaces
{
    public interface ICloudinaryService
    {
        public Task<ServiceResponse<string?>> UploadAvatar(UploadAvatarDTO uploadAvatarDTO);
    }
}
