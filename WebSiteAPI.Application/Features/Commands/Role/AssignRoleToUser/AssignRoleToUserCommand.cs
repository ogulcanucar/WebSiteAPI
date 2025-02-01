using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Commands.Role.AssignRoleToUser
{
    public class AssignRoleToUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public List<string> RoleNames { get; set; }

        public AssignRoleToUserCommand(string userId, List<string> roleNames)
        {
            UserId = userId;
            RoleNames = roleNames;
        }
    }
}
