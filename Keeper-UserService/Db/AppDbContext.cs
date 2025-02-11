using Keeper_UserService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Db
{
    public class AppDbContext : DbContext
    {
        // Tables
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
