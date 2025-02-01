using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
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
            var users = await _userService.GetAllUsersAsync(); // Kullanıcıları çek
            var roles = await _roleService.GetRolesAsync(); // Rolleri çek
            var assignedRoles = string.IsNullOrEmpty(userId) ? new List<string>() : await _userService.GetRolesByUserIdAsync(userId);
            ViewBag.Users = users;
            ViewBag.Roles = roles;
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

            return RedirectToAction("List", "User");
        }

        [HttpGet]
        public async Task<IActionResult> AssignPermissionToRole(string roleId)
        {
            var roles = await _roleService.GetRolesPermissionAsync();
            var permissions = await _permissionService.GetAllPermissionsAsync();
            //var assignedPermissions = roleId != null ? await _permissionService.GetPermissionsByRoleAsync(roleId) : new List<Guid>();
            var assignedPermissions = string.IsNullOrEmpty(roleId) ? new List<Guid>() : await _permissionService.GetPermissionsByRoleAsync(roleId);

            ViewBag.Roles = roles;
            ViewBag.Permissions = permissions;
            ViewBag.AssignedPermissions = assignedPermissions;
            ViewBag.SelectedRoleId = roleId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignPermissionToRole([FromForm] string roleId, [FromForm] List<string> permissionIds)
        {
            if (string.IsNullOrEmpty(roleId) || permissionIds == null || permissionIds.Count == 0)
            {
                ViewBag.Error = "Lütfen bir rol ve en az bir yetki seçin!";
                ViewBag.Roles = await _roleService.GetRolesAsync();
                ViewBag.Permissions = await _permissionService.GetAllPermissionsAsync();
                ViewBag.AssignedPermissions = new List<Guid>();
                ViewBag.SelectedRoleId = roleId;
                return View();
            }

            var allPermissions = await _permissionService.GetAllPermissionsAsync();

            Console.WriteLine("Gelen Permission Ids: " + string.Join(", ", permissionIds));
            Console.WriteLine("Veritabanındaki Yetkiler (ID): " + string.Join(", ", allPermissions.Select(p => p.Id)));

            var selectedPermissions = allPermissions
                .Where(p => permissionIds.Contains(p.Id.ToString())) // ✅ GUID ile eşleştirme yapıyoruz
                .Select(p => p.Id)
                .ToList();

            Console.WriteLine("Seçilen Yetkiler (ID): " + string.Join(", ", selectedPermissions));

            if (!selectedPermissions.Any())
            {
                ViewBag.Error = "Seçilen yetkiler veritabanında bulunamadı!";
                ViewBag.Roles = await _roleService.GetRolesAsync();
                ViewBag.Permissions = await _permissionService.GetAllPermissionsAsync();
                ViewBag.AssignedPermissions = await _permissionService.GetPermissionsByRoleAsync(roleId);
                ViewBag.SelectedRoleId = roleId;
                return View();
            }

            var result = await _permissionService.AssignPermissionToRoleAsync(roleId, selectedPermissions);

            if (!result)
            {
                ViewBag.Error = "Yetki atama işlemi başarısız!";
                ViewBag.Roles = await _roleService.GetRolesAsync();
                ViewBag.Permissions = await _permissionService.GetAllPermissionsAsync();
                ViewBag.AssignedPermissions = await _permissionService.GetPermissionsByRoleAsync(roleId);
                ViewBag.SelectedRoleId = roleId;
                return View();
            }

            return RedirectToAction("AssignPermissionToRole", new { roleId });
        }

    }
}
