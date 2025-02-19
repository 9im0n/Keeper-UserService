using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keeper_UserService.Models.Db
{
    public class ActivationPasswords : BaseModel
    {
        [Required]
        public required Guid UserId { get; set; }

        [Required]
        [JsonIgnore]
        public virtual Users User { get; set; }

        [Required]
        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
