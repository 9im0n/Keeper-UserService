using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;

namespace Keeper_UserService.Services.Interfaces
{
    public interface IDTOMapper
    {
        // Single
        public PermissionDTO Map(Permission permission);
        public RoleDTO Map(Role role);
        public ProfileDTO Map(Profile profile);
        public UserDTO Map(User user);

        // Collections
        public ICollection<PermissionDTO> Map(ICollection<Permission> permissions);
        public ICollection<RoleDTO> Map(ICollection<Role> roles);
        public ICollection<ProfileDTO> Map(ICollection<Profile> profiles);
        public ICollection<UserDTO> Map(ICollection<User> users);
    }
}
