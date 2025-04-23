using Keeper_UserService.Db;
using Keeper_UserService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implementations
{
    public class ProfileRepository: BaseRepository<Profiles>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context): base(context) { }

        public Task<Profiles?> GetByUserIdAsync(Guid userId)
        {
            return _appDbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
