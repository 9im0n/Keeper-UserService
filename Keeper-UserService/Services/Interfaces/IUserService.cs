using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<PagedResultDTO<UserDTO>>> GetPagedAsync(PagedRequestDTO<UserFilterDTO> pagedRequestDTO);
        public Task<ServiceResponse<UserDTO?>> GetByIdAsync(Guid id);
        public Task<ServiceResponse<UserDTO?>> GetByEmailAsync(string email);
        public Task<ServiceResponse<UserDTO?>> CreateAsync(CreateUserDTO newUser);
        public Task<ServiceResponse<UserDTO?>> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDTO);
    }
}
