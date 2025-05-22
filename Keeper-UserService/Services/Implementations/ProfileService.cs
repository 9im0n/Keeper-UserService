using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implementations
{
    public class ProfileService: IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IDTOMapper _mapper;

        public ProfileService(IProfileRepository profileRepository, IDTOMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<PagedResultDTO<ProfileDTO>>> GetProfilesPagedAsync
            (PagedRequestDTO<ProfileFilterDTO> pagedRequestDTO)
        {
            PagedResultDTO<ProfileDTO> result = await _profileRepository.GetPagedProfilesAsync(pagedRequestDTO);
            return ServiceResponse<PagedResultDTO<ProfileDTO>>.Success(result);
        }


        public async Task<ServiceResponse<ProfileDTO?>> GetByIdAsync(Guid id)
        {
            Profile? profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
                return ServiceResponse<ProfileDTO?>.Fail(default, 404, "Profile doesn't exist.");

            ProfileDTO profileDTO = _mapper.Map(profile);

            return ServiceResponse<ProfileDTO?>.Success(profileDTO);
        }


        public async Task<ServiceResponse<ICollection<ProfileDTO>?>> GetBatchedAsync(BatchedProfilesQueryDTO request)
        {
            if (request.profileIds == null)
                return ServiceResponse<ICollection<ProfileDTO>?>.Fail(default, 400, "ProfileIds list cannot be empty.");

            ICollection<Profile> profiles = await _profileRepository.GetBatchedAsync(request.profileIds);

            ICollection<ProfileDTO> profileDTOs = _mapper.Map(profiles);

            return ServiceResponse<ICollection<ProfileDTO>?>.Success(profileDTOs);
        }


        public async Task<ServiceResponse<ProfileDTO?>> CreateAsync(CreateProfileDTO createProfileDTO)
        {
            Profile? oldProfile = await _profileRepository.GetByIdAsync(createProfileDTO.Id);

            if (oldProfile != null)
                return ServiceResponse<ProfileDTO?>.Fail(default, 409, "Profile already exists.");

            Profile newProfile = new Profile
            {
                Name = createProfileDTO.Name,
                Description = createProfileDTO.Description,
                AvatarUrl = createProfileDTO.AvatarUrl,
                Id = createProfileDTO.Id
            };

            newProfile = await _profileRepository.CreateAsync(newProfile);

            ProfileDTO profileDTO = _mapper.Map(newProfile);

            return ServiceResponse<ProfileDTO?>.Success(profileDTO, 201);
        }


        public async Task<ServiceResponse<ProfileDTO?>> UpdateAsync(Guid profileId, Guid userId, UpdateProfileDTO updateProfileDTO)
        {
            Profile? profile = await _profileRepository.GetByIdAsync(profileId);

            if (profile == null)
                return ServiceResponse<ProfileDTO?>.Fail(default, 404, "Profile doesn't exist.");

            if (userId != profileId)
                return ServiceResponse<ProfileDTO?>.Fail(default, 403, "You cannot update another user's profile.");

            Profile newProfile = new Profile
            {
                Id = profileId,
                Name = updateProfileDTO.Name,
                Description = updateProfileDTO.Description,
                AvatarUrl = updateProfileDTO.AvatarUrl,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
            };

            profile = await _profileRepository.UpdateAsync(newProfile);

            ProfileDTO profileDTO = _mapper.Map(profile);

            return ServiceResponse<ProfileDTO?>.Success(profileDTO);
        }
    }
}
