using Keeper_UserService.Db;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IDTOMapper _mapper;

        public UserRepository(AppDbContext appDbContext, IDTOMapper mapper) : base(appDbContext)
        {
            _mapper = mapper;
        }

        public override async Task<List<User>> GetAllAsync()
        {
            return await _appDbContext.Users.Include(u => u.Role).ThenInclude(r => r.Permissions).Include(u => u.DeniedPermissions)
                .Include(u => u.Profile).ToListAsync();
        }

        public override async Task<User?> GetByIdAsync(Guid Id)
        {
            return await _appDbContext.Users.Include(u => u.Role).ThenInclude(r => r.Permissions).Include(u => u.DeniedPermissions)
                .Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == Id);
        }

        public async Task<PagedResultDTO<UserDTO>> GetPagedUsersAsync(PagedRequestDTO<UserFilterDTO> request)
        {
            IQueryable<User> query = _appDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(request.Filter?.Email))
                query = query.Where(u => u.Email.Contains(request.Filter.Email));

            if (request.Filter?.RoleId != null)
                query = query.Where(u => u.RoleId == request.Filter.RoleId);

            int totalCount = await query.CountAsync();

            bool isDescending = request.Direction.ToLower() == "desc";

            query = request.Sort.ToLower() switch
            {
                "id" => isDescending ? query.OrderByDescending(u => u.Id) : query.OrderBy(u => u.Id),
                "email" => isDescending ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                "createdat" => isDescending ? query.OrderByDescending(u => u.CreatedAt) : query.OrderBy(u => u.CreatedAt),
                _ => isDescending ? query.OrderByDescending(u => u.Id) : query.OrderBy(u => u.Id)
            };

            query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            List<UserDTO> userDTOs = _mapper.Map(await query.ToListAsync()).ToList();

            return new PagedResultDTO<UserDTO>()
            {
                Items = userDTOs,
                TotalCount = totalCount
            };
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users.Include(u => u.Role).ThenInclude(r => r.Permissions).Include(u => u.DeniedPermissions)
                .Include(u => u.Profile).FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
