using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.DTOs.Auth;
using WebSiteAPI.Application.DTOs.User;
using WebSiteAPI.Persistence.Services.Authorization;

namespace WebSiteAPI.Presentation.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public RoleController(IRoleService roleService, IUserService userService, IPermissionService permissionService)
        {
            _roleService = roleService;
            _userService = userService;
            _permissionService = permissionService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);

            if (!result)
            {
                ViewBag.Error = "Bu rol zaten mevcut!";
                return View();
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var roles = await _roleService.GetRolesAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string roleName)
        {
            var result = await _roleService.DeleteRoleAsync(roleName);

            if (!result)
            {
                ViewBag.Error = "Rol bulunamadı veya silinemedi!";
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> AssignRoleToUser(string userId)
        {
            var users = await _userService.GetAllUsersAsync();
            var roles = await _roleService.GetRolesAsync();

            var assignedRoles = string.IsNullOrEmpty(userId)
                ? new List<string>()
                : await _userService.GetRolesByUserIdAsync(userId);

            // Eğer gelen roller Role nesnesi içeriyorsa, string listeye çevir
            var roleNames = roles.Select(r => r.ToString()).ToList();

            ViewBag.Users = users;
            ViewBag.Roles = roleNames;  // Role nesnesi yerine string liste
            ViewBag.AssignedRoles = assignedRoles;
            ViewBag.SelectedUserId = userId;

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, List<string> roleNames)
        {
            var result = await _roleService.AssignRoleToUserAsync(userId, roleNames);

            if (!result)
            {
                ViewBag.Error = "Rol atama işlemi başarısız!";
                return View();
            }

            return RedirectToAction("AssignRoleToUser", "Role");
        }

        [HttpGet]
        public async Task<IActionResult> AssignPermissionToRole(string roleId)
        {
            var roles = await _roleService.GetRolesPermissionAsync();
            var permissions = await _permissionService.GetAllPermissionsAsync();

            var assignedPermissions = string.IsNullOrEmpty(roleId)
                ? new List<string>()
                : await _roleService.GetPermissionsByRoleIdAsync(roleId);

            // Eğer roller boşsa, boş bir liste gönder
            ViewBag.Roles = roles ?? new List<AppRoleDto>();
            ViewBag.Permissions = permissions?.Select(p => p.Name).ToList() ?? new List<string>();

            ViewBag.AssignedPermissions = assignedPermissions ?? new List<string>();
            ViewBag.SelectedRoleId = roleId ?? ""; // Boşsa varsayılan değer ata

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AssignPermissionToRole(string roleId, List<string> permissionNames)
        {
            var result = await _roleService.AssignPermissionsToRoleAsync(roleId, permissionNames);

            if (!result)
            {
                ViewBag.Error = "Yetki atama işlemi başarısız!";
                return View();
            }

            return RedirectToAction("AssignPermissionToRole", "Role", new { roleId });
        }


    }
}
