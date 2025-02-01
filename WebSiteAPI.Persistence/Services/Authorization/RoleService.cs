using MediatR;
using Microsoft.AspNetCore.Identity;
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