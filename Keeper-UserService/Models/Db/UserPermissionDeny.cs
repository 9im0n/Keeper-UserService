using System.ComponentModel.DataAnnotations.Schema;

namespace Keeper_UserService.Models.Db
{
    [Table("UserPermissionDenies")]
    public class UserPermissionDeny : BaseModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
