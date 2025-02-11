using System.ComponentModel.DataAnnotations;

namespace Keeper_UserService.Models.DTO
{
    public class CreateUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
