using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a=WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Repositories.Permission
{
    public interface IPermissionWriteRepository : IWriteRepository<a.Permission>
    {
    }
}
