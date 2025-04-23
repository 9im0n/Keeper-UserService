using Keeper_UserService.Models.Db;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keeper_UserService.Models.DTO
{
    public class CreateProfileDTO
    {
        [Required]
        public string Name { get; set; } = "New User";

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Url]
        public string AvatarUrl { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
