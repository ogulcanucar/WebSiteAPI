using MediatR;
using System;
using System.Collections.Generic;

namespace WebSiteAPI.Application.Features.Commands.Permission.AssignPermissionToRole
{
    public class AssignPermissionToRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public List<Guid> PermissionIds { get; set; }

        public AssignPermissionToRoleCommand(string roleId, List<Guid> permissionIds)
        {
            RoleId = roleId;
            PermissionIds = permissionIds;
        }
    }
}
