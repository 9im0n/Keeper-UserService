using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<List<Users>>> GetAllAsync();
        public Task<ServiceResponse<Users?>> GetByIdAsync(Guid id);
        public Task<ServiceResponse<Users?>> GetByEmailAsync(string email);
        public Task<ServiceResponse<Users?>> CreateAsync(CreateUserDTO newUser);
    }
}
