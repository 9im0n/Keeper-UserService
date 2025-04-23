using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implementations
{
    public class ProfileService: IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }


        public async Task<ServiceResponse<Profiles?>> GetByIdAsync(Guid id)
        {
            Profiles? profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
                return ServiceResponse<Profiles?>.Fail(default, 404, "Profile doesn't exist.");

            return ServiceResponse<Profiles?>.Success(profile);
        }

        public async Task<ServiceResponse<Profiles?>> GetByUserIdAsync(Guid id)
        {
            Profiles? profile = await _profileRepository.GetByUserIdAsync(id);

            if (profile == null)
                return ServiceResponse<Profiles?>.Fail(default, 404, "Profile doesn't exist.");

            return ServiceResponse<Profiles?>.Success(profile);
        }

        public async Task<ServiceResponse<Profiles?>> CreateAsync(CreateProfileDTO createProfileDTO)
        {
            Profiles? oldProfile = await _profileRepository.GetByUserIdAsync(createProfileDTO.UserId);

            if (oldProfile != null)
                return ServiceResponse<Profiles?>.Fail(default, 409, "Profile already exists.");

            Profiles newProfile = new Profiles
            {
                Name = createProfileDTO.Name,
                Description = createProfileDTO.Description,
                AvatarUrl = createProfileDTO.AvatarUrl,
                UserId = createProfileDTO.UserId,
            };

            newProfile = await _profileRepository.CreateAsync(newProfile);
            return ServiceResponse<Profiles?>.Success(newProfile, 201);
        }

        public async Task<ServiceResponse<Profiles?>> UpdateAsync(Guid id, UpdateProfileDTO updateProfileDTO)
        {
            Profiles? profile = await _profileRepository.GetByIdAsync(updateProfileDTO.Id);

            if (profile == null)
                return ServiceResponse<Profiles?>.Fail(default, 404, "Profile doesn't exist.");

            if (profile.UserId != id)
                return ServiceResponse<Profiles?>.Fail(default, 403, "You cannot update another user's profile.");

            Profiles newProfile = new Profiles
            {
                Id = updateProfileDTO.Id,
                Name = updateProfileDTO.Name,
                Description = updateProfileDTO.Description,
                AvatarUrl = updateProfileDTO.AvatarUrl,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                UserId = profile.UserId,
            };

            profile = await _profileRepository.UpdateAsync(newProfile);
            return ServiceResponse<Profiles?>.Success(profile);
        }

        public async Task<ServiceResponse<Profiles?>> DeleteAsync(Guid id)
        {
            Profiles? profile = await _profileRepository.DeleteAsync(id);
            return ServiceResponse<Profiles?>.Success(profile);
        }
    }
}
