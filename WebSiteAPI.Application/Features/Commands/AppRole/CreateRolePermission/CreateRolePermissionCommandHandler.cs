using MediatR;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Features.Commands.RolePermission.CreateRolePermission;
using System.Threading;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.Features.Commands.AppRole.CreateRolePermission;

namespace WebSiteAPI.Application.Features.Commands.RolePermission.CreateRolePermission
{
    public class CreateRolePermissionCommandHandler
        : IRequestHandler<CreateRolePermissionCommandRequest, CreateRolePermissionCommandResponse>
    {
        private readonly IRolePermissionService _rolePermissionService;

        public CreateRolePermissionCommandHandler(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        public async Task<CreateRolePermissionCommandResponse> Handle(
            CreateRolePermissionCommandRequest request,
            CancellationToken cancellationToken)
        {
            // Burada IRolePermissionService içindeki "AddPermissionToRoleAsync" metodunu kullanacağız.
            var result = await _rolePermissionService.AddPermissionToRoleAsync(
                request.RoleId,
                // Eğer GUID kullanıyorsan int yerine Guid parametresi gelmeli.
                // Şimdilik senin Request'teki "Permission" int ise, IRolePermissionService de int'e göre olmalı
                Guid.Parse(request.Permission) // veya direkt request.PermissionId
            );

            return new CreateRolePermissionCommandResponse
            {
                Succeeded = result,
                Message = result ? "Permission added to role successfully." : "Permission already exists."
            };
        }
    }
}
