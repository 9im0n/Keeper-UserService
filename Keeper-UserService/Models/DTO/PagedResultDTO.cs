namespace Keeper_UserService.Models.DTO
{
    public class PagedResultDTO<TData>
    {
        public int TotalCount { get; set; }
        public List<TData> Items { get; set; } = null!;
    }
}
