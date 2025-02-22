using Keeper_UserService.Db;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implementations
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Users> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
