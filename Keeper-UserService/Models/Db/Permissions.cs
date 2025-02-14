using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keeper_UserService.Models.Db
{
    public class Permissions : BaseModel
    {
        [Required]
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Roles> Roles { get; set; } = new List<Roles>();

        [JsonIgnore]
        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
