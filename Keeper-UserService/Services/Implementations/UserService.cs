using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;
using BCrypt.Net;

namespace Keeper_UserService.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolesService _rolesService;
        private readonly IProfileService _profileService;
        private readonly IDTOMapper _mapper;


        public UserService(IUserRepository userRepository, 
                           IRolesService rolesService, 
                           IProfileService profileService,
                           IDTOMapper mapper)
        {
            _userRepository = userRepository;
            _rolesService = rolesService;
            _profileService = profileService;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<PagedResultDTO<UserDTO>>> GetPagedAsync(
            PagedRequestDTO<UserFilterDTO> pagedRequestDTO)
        {
            PagedResultDTO<UserDTO> result = await _userRepository.GetPagedUsersAsync(pagedRequestDTO);
            return ServiceResponse<PagedResultDTO<UserDTO>>.Success(result);
        }


        public async Task<ServiceResponse<UserDTO?>> GetByIdAsync(Guid id)
        {
            User? user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return ServiceResponse<UserDTO>.Fail(null, 404, "User with this id doesn't exist");

            UserDTO userDTO = _mapper.Map(user);

            return ServiceResponse<UserDTO>.Success(userDTO);
        }


        public async Task<ServiceResponse<UserDTO?>> GetByEmailAsync(string email)
        {
            User? user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return ServiceResponse<UserDTO>.Fail(null, 404, "User with this email doesn't exist");

            UserDTO userDTO = _mapper.Map(user);

            return ServiceResponse<UserDTO>.Success(userDTO);
        }


        public async Task<ServiceResponse<UserDTO?>> CreateAsync(CreateUserDTO newUser)
        {
            User? oldUser = await _userRepository.GetByEmailAsync(newUser.Email);

            if (oldUser != null)
                return ServiceResponse<UserDTO>.Fail(default, 409, "User with this email is already exists.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password, workFactor: 12);
            var role = await _rolesService.GetUserRoleAsync();

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = newUser.Email,
                Password = hashedPassword,
                RoleId = role.Data.Id,
            };

            User User = await _userRepository.CreateAsync(user);

            CreateProfileDTO createProfileDTO = new CreateProfileDTO { Id = User.Id };
            ServiceResponse<ProfileDTO?> profile = await _profileService.CreateAsync(createProfileDTO);

            if (!profile.IsSuccess)
                return ServiceResponse<UserDTO>.Fail(default, profile.Status, profile.Message);

            UserDTO userDTO = _mapper.Map(User);

            return ServiceResponse<UserDTO>.Success(userDTO, 201);
        }

        public async Task<ServiceResponse<UserDTO?>> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDTO)
        {
            User? user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                return ServiceResponse<UserDTO?>.Fail(default, 404, "User don't exist.");

            user.RoleId = updateUserDTO.RoleId;
            user = await _userRepository.UpdateAsync(user);

            UserDTO userDTO = _mapper.Map(user);
            return ServiceResponse<UserDTO?>.Success(userDTO);
        }
    }
}
