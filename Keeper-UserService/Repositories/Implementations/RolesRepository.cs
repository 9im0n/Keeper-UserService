using Keeper_UserService.Db;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implementations
{
    public class RolesRepository : BaseRepository<Role>, IRolesRepository
    {
        public RolesRepository(AppDbContext appDbContext) : base(appDbContext) { }


        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _appDbContext.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
