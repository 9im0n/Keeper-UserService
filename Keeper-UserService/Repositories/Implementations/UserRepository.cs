using Keeper_UserService.Db;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implementations
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public override async Task<List<Users>> GetAllAsync()
        {
            return await _appDbContext.Users.Include(u => u.Role).Include(u => u.Permissions).ToListAsync();
        }

        public override async Task<Users?> GetByIdAsync(Guid Id)
        {
            return await _appDbContext.Users.Include(u => u.Role)
                .Include(u => u.Permissions).FirstOrDefaultAsync(u => u.Id == Id);
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users.Include(u => u.Role)
                .Include(u => u.Permissions).FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
