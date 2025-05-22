using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implementations
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<ServiceResponse<string?>> UploadAvatar(UploadAvatarDTO uploadAvatarDTO)
        {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(uploadAvatarDTO.Avatar.FileName, uploadAvatarDTO.Avatar.OpenReadStream()),
                Folder = "Keeper/ProfileImages"
            };

            ImageUploadResult result = await _cloudinary.UploadAsync(uploadParams);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                return ServiceResponse<string?>.Fail(default, (int)result.StatusCode, result.Error.Message);

            return ServiceResponse<string?>.Success(result.SecureUrl.ToString());
        }
    }
}
