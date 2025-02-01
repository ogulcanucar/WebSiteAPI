using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.Features.Commands.RolePermission;

namespace WebSiteAPI.Application.Features.Handlers.RolePermission
{
    public class GetPermissionsByRoleQueryHandler : IRequestHandler<GetPermissionsByRoleQueryRequest, GetPermissionsByRoleQueryResponse>
    {
        private readonly IRolePermissionService _rolePermissionService;

        public GetPermissionsByRoleQueryHandler(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        public async Task<GetPermissionsByRoleQueryResponse> Handle(GetPermissionsByRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var permissions = await _rolePermissionService.GetPermissionsByRoleAsync(request.RoleId);
            return new GetPermissionsByRoleQueryResponse { Permissions = permissions };
        }
    }
}
