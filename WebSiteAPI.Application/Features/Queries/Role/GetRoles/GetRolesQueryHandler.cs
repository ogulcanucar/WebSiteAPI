using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a = WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<string>>
    {
        private readonly RoleManager<a.AppRole> _roleManager;

        public GetRolesQueryHandler(RoleManager<a.AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return _roleManager.Roles.Select(r => r.Name).ToList();
        }
    }
}
