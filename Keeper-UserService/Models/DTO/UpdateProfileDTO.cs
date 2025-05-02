using System.ComponentModel.DataAnnotations;

namespace Keeper_UserService.Models.DTO
{
    public class UpdateProfileDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [Url]
        public string AvatarUrl { get; set; } = null!;
    }
}
