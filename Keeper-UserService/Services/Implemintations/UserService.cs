using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implemintations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolesService _rolesService;
        private readonly IActivationPasswordService _activationPasswordService;


        public UserService(IUserRepository userRepository, IRolesService rolesService, 
            IActivationPasswordService activationPasswordService)
        {
            _userRepository = userRepository;
            _rolesService = rolesService;
            _activationPasswordService = activationPasswordService;
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
            Users user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return ServiceResponse<Users>.Fail(null, 404, "User with this email doesn't exist");

            return ServiceResponse<Users>.Success(user);
        }


        public async Task<ServiceResponse<Users?>> CreateAsync(CreateUserDTO newUser)
        {
            Users oldUser = await _userRepository.GetByEmailAsync(newUser.Email);

            if (oldUser != null)
                return ServiceResponse<Users>.Fail(null, 409, "User with this email is already exists.");

            var role = await _rolesService.GetByNameAsync("User");

            Users user = new Users()
            {
                Id = Guid.NewGuid(),
                Email = newUser.Email,
                Password = newUser.Password,
                IsActive = false,
                RoleId = role.Id,
                Role = role,
                Permissions = new List<Permissions>()
            };

            Users User = await _userRepository.CreateAsync(user);

            ServiceResponse<ActivationPasswords> password = await _activationPasswordService.CreateAsync(user);

            // TODO: Отправка пароля на email

            return ServiceResponse<Users>.Success(User, 201);
        }
    }
}
