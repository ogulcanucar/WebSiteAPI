using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories.Role;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Commands.Permission.AssignPermissionToRole
{
    public class AssignPermissionToRoleHandler : IRequestHandler<AssignPermissionToRoleCommand, bool>
    {
        private readonly IRolePermissionReadRepository _rolePermissionReadRepository;
        private readonly IRolePermissionWriteRepository _rolePermissionWriteRepository;

        public AssignPermissionToRoleHandler(IRolePermissionReadRepository rolePermissionReadRepository, IRolePermissionWriteRepository rolePermissionWriteRepository)
        {
            _rolePermissionReadRepository = rolePermissionReadRepository;
            _rolePermissionWriteRepository = rolePermissionWriteRepository;
        }

        public async Task<bool> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
        {
            var existingPermissions = await _rolePermissionReadRepository.GetWhere(rp => rp.RoleId.ToString() == request.RoleId).ToListAsync();

            _rolePermissionWriteRepository.RemoveRange(existingPermissions);
            await _rolePermissionWriteRepository.SaveAsync();

            var newPermissions = request.PermissionIds.Select(pid => new AppRolePermission
            {
                RoleId = Guid.Parse(request.RoleId),
                PermissionId = pid
            }).ToList();

            await _rolePermissionWriteRepository.AddRangeAsync(newPermissions);
            return await _rolePermissionWriteRepository.SaveAsync() > 0;
        }
    }
}
