using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommand : IRequest<bool>
    {
        public string RoleName { get; set; }

        public CreateRoleCommand(string roleName)
        {
            RoleName = roleName;
        }
    }
}
