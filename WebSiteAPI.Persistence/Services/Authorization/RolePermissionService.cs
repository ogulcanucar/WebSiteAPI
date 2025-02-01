using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Application.Repositories.Permission;
using WebSiteAPI.Application.Repositories.Role;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Persistence.Services.Authorization
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionWriteRepository _rolePermissionWriteRepository;
        private readonly IRolePermissionReadRepository _rolePermissionReadRepository;
        private readonly IPermissionReadRepository _permissionReadRepository;

        public RolePermissionService(
            IRolePermissionWriteRepository rolePermissionWriteRepository,
            IRolePermissionReadRepository rolePermissionReadRepository,
            IPermissionReadRepository permissionReadRepository)
        {
            _rolePermissionWriteRepository = rolePermissionWriteRepository;
            _rolePermissionReadRepository = rolePermissionReadRepository;
            _permissionReadRepository = permissionReadRepository;
        }

        public async Task<List<string>> GetPermissionsByRoleAsync(Guid roleId)
        {
            var permissionIds = await _rolePermissionReadRepository
                .GetWhere(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync();

            var permissions = await _permissionReadRepository
                .GetWhere(p => permissionIds.Contains(p.Id))
                .Select(p => p.Name)
                .ToListAsync();

            return permissions;
        }

        public async Task<bool> AddPermissionToRoleAsync(Guid roleId, Guid permissionId)
        {
            var exists = await _rolePermissionReadRepository
                .GetWhere(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)
                .AnyAsync();

            if (exists) return false;

            await _rolePermissionWriteRepository.AddAsync(new AppRolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            });

            await _rolePermissionWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var rolePermission = await _rolePermissionReadRepository
                .GetWhere(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)
                .FirstOrDefaultAsync();

            if (rolePermission == null) return false;

            _rolePermissionWriteRepository.Remove(rolePermission);
            await _rolePermissionWriteRepository.SaveAsync();
            return true;
        }
        public async Task<List<Guid>> GetPermissionsByRoleAsync(string roleId)
        {
            var permissions = await _rolePermissionReadRepository.GetWhere(rp => rp.RoleId.ToString() == roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync();

            return permissions;
        }
    }
}
