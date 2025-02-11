using System.ComponentModel.DataAnnotations;

namespace Keeper_UserService.Models.Db
{
    public class Permissions : BaseModel
    {
        [Required]
        public required string Name { get; set; }

        public ICollection<Roles> Roles { get; set; } = new List<Roles>();

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
