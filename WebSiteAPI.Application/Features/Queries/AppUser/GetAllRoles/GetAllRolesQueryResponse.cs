using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Queries.AppRole.GetAllRoles
{
    public class GetAllRolesQueryResponse
    {
        public Guid Id { get; set; }  // Role ID (GUID)
        public string Name { get; set; } // Role Adı
    }

}
