using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace Keeper_UserService.Models.Db
{
    [Table("Permissions")]
    public class Permission : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<UserPermissionDeny> DeniedForUsers { get; set; } = new List<UserPermissionDeny>();
    }
}
