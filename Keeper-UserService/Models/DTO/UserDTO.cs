namespace Keeper_UserService.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public RoleDTO Role { get; set; } = null!;
        public ProfileDTO Profile { get; set; } = null!;
    }
}
