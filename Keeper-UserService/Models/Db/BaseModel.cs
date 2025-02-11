using System.ComponentModel.DataAnnotations;

namespace Keeper_UserService.Models.Db
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
