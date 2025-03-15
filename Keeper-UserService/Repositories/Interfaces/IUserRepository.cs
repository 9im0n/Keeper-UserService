using Keeper_UserService.Models.Db;

namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        public Task<Users?> GetByEmailAsync(string email);
    }
}
