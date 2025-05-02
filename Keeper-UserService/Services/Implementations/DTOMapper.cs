using Keeper_UserService.Models.Db;
using Keeper_UserService.Models.DTO;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implementations
{
    public class DTOMapper : IDTOMapper
    {
        public PermissionDTO Map(Permission permission)
        {
            return new PermissionDTO()
            {
                Id = permission.Id,
                Name = permission.Name
            };
        }

        public RoleDTO Map(Role role)
        {
            return new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public ProfileDTO Map(Profile profile)
        {
            return new ProfileDTO()
            {
                Id= profile.Id,
                Name = profile.Name,
                Description = profile.Description,
                AvatarUrl = profile.AvatarUrl
            };
        }

        public UserDTO Map(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Role = Map(user.Role),
                Profile = Map(user.Profile)
            };
        }

        public ICollection<PermissionDTO> Map(ICollection<Permission> permissions) => permissions.Select(Map).ToList();

        public ICollection<RoleDTO> Map(ICollection<Role> roles) => roles.Select(Map).ToList();

        public ICollection<ProfileDTO> Map(ICollection<Profile> profiles) => profiles.Select(Map).ToList();

        public ICollection<UserDTO> Map(ICollection<User> users) => users.Select(Map).ToList();
    }
}
