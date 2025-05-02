using Keeper_UserService.Db;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implementations
{
    public class ProfileRepository: BaseRepository<Profile>, IProfileRepository
    {
        private readonly IDTOMapper _mapper;

        public ProfileRepository(AppDbContext context, IDTOMapper mapper): base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<ProfileDTO>> GetPagedProfilesAsync(PagedRequestDTO<ProfileFilterDTO> request)
        {
            IQueryable<Profile> query = _appDbContext.Profiles.AsQueryable();

            if (!string.IsNullOrEmpty(request.Filter?.Name))
                query = query.Where(p => p.Name.Contains(request.Filter.Name));

            int totalCount = await query.CountAsync();

            bool isDescending = request.Direction.ToLower() == "desc";

            query = request.Sort.ToLower() switch
            {
                "Id" => isDescending ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                "createdat" => isDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                _ => isDescending ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id)
            };

            query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            List<ProfileDTO> profileDTOs = _mapper.Map(await query.ToListAsync()).ToList();

            return new PagedResultDTO<ProfileDTO>()
            {
                Items = profileDTOs,
                TotalCount = totalCount
            };
        }
    }
}
