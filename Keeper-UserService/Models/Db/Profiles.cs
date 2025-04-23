using Keeper_UserService.Models.Db;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Profiles : BaseModel
{
    [Required]
    public string Name { get; set; } = string.Empty;  

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Url]
    public string AvatarUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
}
