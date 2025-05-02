using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Repositories.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        public Task<PagedResultDTO<ProfileDTO>> GetPagedProfilesAsync(PagedRequestDTO<ProfileFilterDTO> request);
    }
}
