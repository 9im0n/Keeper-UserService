using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keeper_UserService.Models.Db
{
    public class Roles : BaseModel
    {
        [Required]
        public required string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Permissions> Permissions { get; set; } = new List<Permissions>();

        [JsonIgnore]
        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
