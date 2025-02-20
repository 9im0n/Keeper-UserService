using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<ServiceResponse<string>> SendWelcomeEmailAsync(string email, ActivationPasswords password);
    }
}
