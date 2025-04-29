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
        private readonly IActivationPasswordService _activationPasswordService;
        private readonly IEmailService _emailService;
        private readonly IProfileService _profileService;


        public UserService(IUserRepository userRepository, 
                           IRolesService rolesService, 
                           IActivationPasswordService activationPasswordService, 
                           IEmailService emailService,
                           IProfileService profileService)
        {
            _userRepository = userRepository;
            _rolesService = rolesService;
            _activationPasswordService = activationPasswordService;
            _emailService = emailService;
            _profileService = profileService;
        }


        public async Task<ServiceResponse<List<Users>>> GetAllAsync()
        {
            List<Users> users = await _userRepository.GetAllAsync();
            return ServiceResponse<List<Users>>.Success(users);
        }


        public async Task<ServiceResponse<Users?>> GetByIdAsync(Guid id)
        {
            Users user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return ServiceResponse<Users>.Fail(null, 404, "User with this id doesn't exist");


            return ServiceResponse<Users>.Success(user);
        }


        public async Task<ServiceResponse<Users?>> GetByEmailAsync(string email)
        {
            Users? user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return ServiceResponse<Users>.Fail(null, 404, "User with this email doesn't exist");

            return ServiceResponse<Users>.Success(user);
        }


        public async Task<ServiceResponse<Users?>> CreateAsync(CreateUserDTO newUser)
        {
            if (newUser.Password != newUser.Confirm)
                return ServiceResponse<Users>.Fail(default, 400, "Password and Confirm password don't mutch.");

            Users oldUser = await _userRepository.GetByEmailAsync(newUser.Email);

            if (oldUser != null)
                return ServiceResponse<Users>.Fail(default, 409, "User with this email is already exists.");

            ServiceResponse<ActivationPasswords> password = await _activationPasswordService.CreateAsync(newUser.Email);
            ServiceResponse<string> response = await _emailService.SendWelcomeEmailAsync(newUser.Email, password.Data);

            if (!response.IsSuccess)
                return ServiceResponse<Users>.Fail(default, response.Status, response.Message);

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password, workFactor: 12);
            var role = await _rolesService.GetByNameAsync("User");

            Users user = new Users()
            {
                Id = Guid.NewGuid(),
                Email = newUser.Email,
                Password = hashedPassword,
                IsActive = false,
                RoleId = role.Id,
                Role = role,
                Permissions = new List<Permissions>()
            };

            Users User = await _userRepository.CreateAsync(user);

            CreateProfileDTO createProfileDTO = new CreateProfileDTO { UserId = User.Id };
            ServiceResponse<Profiles?> profile = await _profileService.CreateAsync(createProfileDTO);

            if (!profile.IsSuccess)
                return ServiceResponse<Users>.Fail(default, profile.Status, profile.Message);

            return ServiceResponse<Users>.Success(User, 201);
        }


        public async Task<ServiceResponse<Users?>> ActivateUser(UserActivationDTO activation)
        {
            Users? user = await _userRepository.GetByEmailAsync(activation.Email);

            if (user == null)
                return ServiceResponse<Users?>.Fail(default, 404, "User doesn't exist with this email.");

            ServiceResponse<ActivationPasswords?> password = await _activationPasswordService.GetByEmailAsync(user.Email);

            if (!password.IsSuccess)
                return ServiceResponse<Users?>.Fail(default, 404, "Activation password doesn't exist.");

            if (password.Data.Password != activation.ActivationPassword)
                return ServiceResponse<Users?>.Fail(default, 400, "Activation passwords are not same.");

            user.IsActive = true;
            user = await _userRepository.UpdateAsync(user);

            return ServiceResponse<Users?>.Success(user, 201, "The user has been activated.");
        }
    }
}
