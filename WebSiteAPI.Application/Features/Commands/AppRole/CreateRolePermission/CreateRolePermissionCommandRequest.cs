using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Commands.AppRole.CreateRolePermission
{
    public class CreateRolePermissionCommandRequest : IRequest<CreateRolePermissionCommandResponse>
    {
        public Guid RoleId { get; set; }
        public string Page { get; set; }  // Yetkinin ait olduğu sayfa
        public string Permission { get; set; }  // Yetki türü (View, Edit, Delete vs.)
    }
}
