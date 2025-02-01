using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Repositories.Role
{
    public interface IRolePermissionReadRepository : IReadRepository<AppRolePermission>
    {
    }
}
