using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Abstractions.Service.Authorization
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAllPermissionsAsync();
        Task AddPermissionAsync(Permission permission);
        Task DeletePermissionAsync(Guid permissionId);
        Task<bool> AssignPermissionToRoleAsync(string roleId, List<Guid> permissionIds);
        Task<List<Guid>> GetPermissionsByRoleAsync(string roleId); 

    }
}
