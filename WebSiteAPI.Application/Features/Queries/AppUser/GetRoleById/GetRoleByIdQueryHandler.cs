using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSiteAPI.Application.Features.Queries.AppRole.GetRoleById;
using a= WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Handlers.AppRole.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        private readonly RoleManager<a.AppRole> _roleManager;

        public GetRoleByIdQueryHandler(RoleManager<a.AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return null; // Eğer rol bulunamazsa null dönebiliriz

            return new GetRoleByIdQueryResponse
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
