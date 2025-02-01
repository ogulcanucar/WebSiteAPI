using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a = WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly RoleManager<a.AppRole> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<a.AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
                return false; // Eğer rol bulunamazsa, false döndür.

            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded;
        }
    }
}
