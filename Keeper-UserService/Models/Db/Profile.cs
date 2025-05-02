using Keeper_UserService.Models.Db;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Keeper_UserService.Models.Db
{
    [Table("Profiles")]
    public class Profile : BaseModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [Url]
        public string AvatarUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
    }
}
