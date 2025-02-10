using WebSiteAPI.Application.DTOs.Auth;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Abstractions.Service.Authorization
{
    public interface IRoleService
    {
        Task<bool> CreateRoleAsync(string roleName);
        Task<List<string>> GetRolesAsync();
        Task<bool> DeleteRoleAsync(string roleName);
        Task<bool> AssignRoleToUserAsync(string userId, List<string> roleNames);
        Task<List<AppRoleDto>> GetRolesPermissionAsync();
        Task<List<string>> GetPermissionsByRoleIdAsync(string roleId);
        Task<bool> AssignPermissionsToRoleAsync(string roleId, List<string> permissionNames);
    }
}
