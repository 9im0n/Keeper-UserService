using System.ComponentModel.DataAnnotations;

namespace Keeper_UserService.Models.Db
{
    public class Roles : BaseModel
    {
        [Required]
        public required string Name { get; set; }

        public virtual ICollection<Permissions> Permissions { get; set; } = new List<Permissions>();
        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
