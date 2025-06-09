namespace Keeper_UserService.Models.DTO
{
    public class BatchedProfilesQueryDTO
    {
        public ICollection<Guid>? profileIds { get; set; }
    }
}
