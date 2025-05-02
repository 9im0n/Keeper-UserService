using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Keeper_UserService.Models.Db
{
    [Table("Roles")]
    public class Role : BaseModel
    {
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
