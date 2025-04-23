using System.ComponentModel.DataAnnotations;

namespace Keeper_UserService.Models.DTO
{
    public class UpdateProfileDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = "New User";

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Url]
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
