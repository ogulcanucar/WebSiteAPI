using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.Features.Commands.Permission.AssignPermissionToRole;
using WebSiteAPI.Application.Repositories.Permission;
using WebSiteAPI.Application.Repositories.Role;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Persistence.Services.Authorization
{
    public class PermissionService : IPermissionService
    {
        private readonly IMediator _mediator;
        private readonly IPermissionReadRepository _permissionReadRepository;
        private readonly IPermissionWriteRepository _permissionWriteRepository;
        private readonly IRolePermissionReadRepository _rolePermissionReadRepository;

        public PermissionService(IPermissionReadRepository permissionReadRepository, IPermissionWriteRepository permissionWriteRepository,IMediator mediator, IRolePermissionReadRepository rolePermissionReadRepository)
        {
            _permissionReadRepository = permissionReadRepository;
            _permissionWriteRepository = permissionWriteRepository;
            _mediator = mediator;
            _rolePermissionReadRepository= rolePermissionReadRepository;
        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return _permissionReadRepository.GetAll().ToList();
        }

        public async Task AddPermissionAsync(Permission permission)
        {
            await _permissionWriteRepository.AddAsync(permission);
            await _permissionWriteRepository.SaveAsync();
        }

        public async Task DeletePermissionAsync(Guid permissionId)
        {
            var permission = await _permissionReadRepository.GetByIdAsync(permissionId.ToString());
            if (permission != null)
            {
                _permissionWriteRepository.Remove(permission);
                await _permissionWriteRepository.SaveAsync();
            }
        }


        public async Task<bool> AssignPermissionToRoleAsync(string roleId, List<Guid> permissionIds)
        {
            return await _mediator.Send(new AssignPermissionToRoleCommand(roleId, permissionIds));
        }

        public async Task<List<Guid>> GetPermissionsByRoleAsync(string roleId)
        {
            var roleGuid = Guid.Parse(roleId);

            return await _rolePermissionReadRepository
                .GetWhere(rp => rp.RoleId == roleGuid)
                .Select(rp => rp.PermissionId)
                .ToListAsync();
        }
    }
}

