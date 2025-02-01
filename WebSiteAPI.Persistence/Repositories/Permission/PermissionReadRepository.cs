using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories.Permission;
using WebSiteAPI.Persistence.Contexts;
using a= WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Persistence.Repositories.Permission
{
    public class PermissionReadRepository : ReadRepository<a.Permission>, IPermissionReadRepository
    {
        public PermissionReadRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
