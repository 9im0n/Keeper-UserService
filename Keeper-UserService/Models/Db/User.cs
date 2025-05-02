using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keeper_UserService.Models.Db
{
    [Table("Users")]
    public class User : BaseModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [MinLength(8)]
        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public Guid RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public Profile? Profile { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public ICollection<UserPermissionDeny> DeniedPermissions { get; set; } = new List<UserPermissionDeny>();
    }
}
