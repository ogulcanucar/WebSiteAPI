using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<AppRolePermission> RolePermissions { get; set; } = new List<AppRolePermission>();
    }
}
