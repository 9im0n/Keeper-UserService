using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IActivationPasswordsRepository : IBaseRepository<ActivationPasswords>
    {
        public Task<ActivationPasswords?> GetByUserIdAsync(Guid Id);
    }
}
