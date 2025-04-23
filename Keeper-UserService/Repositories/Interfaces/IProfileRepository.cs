namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Profiles>
    {
        public Task<Profiles?> GetByUserIdAsync(Guid userId);
    }
}
