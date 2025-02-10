using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.DTOs.Auth;
using WebSiteAPI.Application.Features.Commands.Role.AssignRoleToUser;
using WebSiteAPI.Application.Features.Commands.Role.CreateRole;
using WebSiteAPI.Application.Features.Commands.Role.DeleteRole;
using WebSiteAPI.Application.Features.Queries.Role.GetRoles;
using WebSiteAPI.Domain.Entities.Identity;

public class RoleService : IRoleService
{
    private readonly IMediator _mediator;
    private readonly RoleManager<AppRole> _roleManager;

    public RoleService(IMediator mediator, RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
        _mediator = mediator;
    }

    public async Task<bool> AssignPermissionsToRoleAsync(string roleId, List<string> permissionNames)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return false;
        }

        var existingClaims = await _roleManager.GetClaimsAsync(role);

        var claimsToAdd = permissionNames.Except(existingClaims.Select(c => c.Value)).ToList();
        var claimsToRemove = existingClaims.Where(c => !permissionNames.Contains(c.Value)).ToList();

        foreach (var claim in claimsToRemove)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        foreach (var claim in claimsToAdd)
        {
            await _roleManager.AddClaimAsync(role, new Claim("Permission", claim));
        }

        return true;
    }

    public async Task<bool> AssignRoleToUserAsync(string userId, List<string> roleNames)
    {
        return await _mediator.Send(new AssignRoleToUserCommand(userId, roleNames));
    }

    public async Task<bool> CreateRoleAsync(string roleName)
    {
        return await _mediator.Send(new CreateRoleCommand(roleName));
    }

    public async Task<bool> DeleteRoleAsync(string roleName)
    {
        return await _mediator.Send(new DeleteRoleCommand(roleName));
    }

    public async Task<List<string>> GetPermissionsByRoleIdAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return new List<string>();
        }

        var claims = await _roleManager.GetClaimsAsync(role); // Role ait yetkileri (claimleri) getir

        return claims.Select(c => c.Value).ToList(); // Claim değerlerini string olarak döndür
    }

    public async Task<List<string>> GetRolesAsync()
    {
        return await _mediator.Send(new GetRolesQuery());
    }


    public async Task<List<AppRoleDto>> GetRolesPermissionAsync()
    {
        return _roleManager.Roles.Select(r => new AppRoleDto
        {
            Id = r.Id.ToString(),
            Name = r.Name
        }).ToList();
    }
}