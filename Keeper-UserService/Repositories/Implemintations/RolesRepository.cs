using Keeper_UserService.Db;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implemintations
{
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        public RolesRepository(AppDbContext appDbContext) : base(appDbContext) { }


        public async Task<Roles?> GetByNameAsync(string name)
        {
            return await _appDbContext.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
