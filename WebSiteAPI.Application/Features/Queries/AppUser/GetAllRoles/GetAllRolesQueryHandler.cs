using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a=WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Queries.AppRole.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, List<GetAllRolesQueryResponse>>
    {
        private readonly RoleManager<a.AppRole> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<a.AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<GetAllRolesQueryResponse>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.Select(r => new GetAllRolesQueryResponse
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return roles;
        }
    }
}
