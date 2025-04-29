using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IActivationPasswordService
    {
        public Task<ServiceResponse<ActivationPasswords?>> GetByEmailAsync(string email);
        public Task<ServiceResponse<ActivationPasswords>> CreateAsync(string email);
    }
}
