using System.Globalization;

namespace Keeper_UserService.Models.Services
{
    public class CloudinarySettings
    {
        public string ApiKey { get; set; } = null!;
        public string ApiSecret { get; set;} = null!;
        public string CloudName { get; set; } = null!;
    }
}
