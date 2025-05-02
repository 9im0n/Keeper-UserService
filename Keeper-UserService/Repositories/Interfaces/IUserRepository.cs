using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<PagedResultDTO<UserDTO>> GetPagedUsersAsync(PagedRequestDTO<UserFilterDTO> request);
        public Task<User?> GetByEmailAsync(string email);
    }
}
