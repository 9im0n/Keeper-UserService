using Keeper_UserService.Db;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implementations
{
    public class ActivationPasswordsRepository : BaseRepository<ActivationPasswords>, IActivationPasswordsRepository
    {
        public ActivationPasswordsRepository(AppDbContext context): base(context) { }


        public async Task<ActivationPasswords?> GetByUserIdAsync(Guid Id)
        {
            return await _appDbContext.ActivationPasswords.FirstOrDefaultAsync(p => p.UserId == Id);
        }
    }
}
