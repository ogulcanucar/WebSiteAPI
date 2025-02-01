using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a = WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Commands.Role.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, bool>
    {
        private readonly UserManager<a.AppUser> _userManager;

        public AssignRoleToUserCommandHandler(UserManager<a.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return false; // Kullanıcı bulunamazsa false döndür.

            // Kullanıcıya atanmış mevcut rolleri kaldır
            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            // Yeni rolleri ata
            var result = await _userManager.AddToRolesAsync(user, request.RoleNames);

            return result.Succeeded;
        }
    }
}
