using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a= WebSiteAPI.Domain.Entities  ;

namespace WebSiteAPI.Application.Repositories.Permission
{
    public interface IPermissionReadRepository : IReadRepository<a.Identity.Permission>
    {
    }
}
