using MediatR;
using Microsoft.AspNetCore.Identity;
using a = WebSiteAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly RoleManager<a.AppRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<a.AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
            if (roleExists)
                return false; // Eğer rol zaten varsa, eklememek için false döndürüyoruz.

            var result = await _roleManager.CreateAsync(new a.AppRole { Name = request.RoleName });

            return result.Succeeded;
        }
    }
}
