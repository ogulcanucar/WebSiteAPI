using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories.Role;
using WebSiteAPI.Domain.Entities.Identity;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories.Role
{
    public class RolePermissionReadRepository : ReadRepository<AppRolePermission>, IRolePermissionReadRepository
    {
        public RolePermissionReadRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
