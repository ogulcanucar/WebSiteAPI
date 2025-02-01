using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public string RoleName { get; set; }

        public DeleteRoleCommand(string roleName)
        {
            RoleName = roleName;
        }
    }
}
