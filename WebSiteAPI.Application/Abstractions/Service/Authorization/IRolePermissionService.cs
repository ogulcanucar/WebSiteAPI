using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Abstractions.Service.Authorization
{
    public interface IRolePermissionService
    {
        Task<List<string>> GetPermissionsByRoleAsync(Guid roleId); // Role ait izinleri getir
        Task<bool> AddPermissionToRoleAsync(Guid roleId, Guid permissionId); // Role izin ekle
        Task<bool> RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId); // Role izni kaldır
        Task<List<Guid>> GetPermissionsByRoleAsync(string roleId);
    }
}
