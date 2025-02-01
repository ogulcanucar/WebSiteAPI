using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories.Permission;
using a= WebSiteAPI.Domain.Entities.Identity;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories.Permission
{
    public class PermissionWriteRepository : WriteRepository<a.Permission>, IPermissionWriteRepository
    {
        public PermissionWriteRepository(WebSiteAPIContext context) : base(context) { }
    }
}
