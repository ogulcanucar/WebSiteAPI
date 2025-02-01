using MediatR;
using System;
using System.Collections.Generic;
using WebSiteAPI.Application.Features.Handlers.RolePermission;

namespace WebSiteAPI.Application.Features.Commands.RolePermission
{
    public class GetPermissionsByRoleQueryRequest : IRequest<GetPermissionsByRoleQueryResponse>
    {
        public Guid RoleId { get; set; }
    }
}
