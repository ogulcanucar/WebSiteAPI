using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a= WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Commands.AppRole.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        private readonly RoleManager<a.AppRole> _roleManager;

        public UpdateRoleCommandHandler(RoleManager<a.AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
            {
                return new UpdateRoleCommandResponse
                {
                    Succeeded = false,
                    Message = "Rol bulunamadı!"
                };
            }

            role.Name = request.NewRoleName;
            var result = await _roleManager.UpdateAsync(role);

            return new UpdateRoleCommandResponse
            {
                Succeeded = result.Succeeded,
                Message = result.Succeeded ? "Rol başarıyla güncellendi." : "Rol güncellenirken hata oluştu!"
            };
        }
    }
}
