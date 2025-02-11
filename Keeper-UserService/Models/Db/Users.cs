using System.ComponentModel.DataAnnotations;

namespace Keeper_UserService.Models.Db
{
    public class Users : BaseModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        [MinLength(8)]
        public required string Password { get; set; }

        public bool IsActive { get; set; } = false;

        public required Guid RoleId { get; set; }
        public virtual Roles Role { get; set; }

        public virtual ICollection<Permissions> Permissions { get; set; } = new List<Permissions>();


        public IEnumerable<Permissions> GetPermissions() => Role.Permissions.Union(Permissions);
    }
}
