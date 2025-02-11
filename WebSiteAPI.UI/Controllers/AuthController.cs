using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebSiteAPI.Application.Features.Commands.Auth.Login;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthController(IMediator mediator, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // MediatR üzerinden LoginCommand'i çalıştırıyoruz
            var result = await _mediator.Send(new LoginCommand(username, password));

            // Eğer result null döndüyse, kullanıcı adı/şifre hatalı
            if (result == null)
            {
                ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
                return View();
            }

            // Kullanıcı bilgilerini veritabanından çekiyoruz
            var user = await _userManager.FindByIdAsync(result.UserId.ToString());
            if (user == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı!";
                return View();
            }

            // 1) Claims listesi oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ?? Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? "UnknownUser")
            };

            // 2) Kullanıcının rollerini ekle
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                Console.WriteLine($"🟢 Kullanıcı Rolü: {role}");

                // Örnek: Admin rolüne özel bir permission ekleyelim
                if (role == "Admin")
                    claims.Add(new Claim("Permission", "AdminAccess"));
            }

            // 3) Kullanıcının veritabanında tanımlı yetkilerini al (Permission Claim)
            var userPermissions = await GetUserPermissionsAsync(user);
            foreach (var permission in userPermissions)
            {
                claims.Add(new Claim("Permission", permission));
                Console.WriteLine($"🟢 Kullanıcı Yetkisi: {permission}");
            }

            // 4) ClaimsIdentity ve ClaimsPrincipal oluştur
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 5) AuthenticationProperties ayarla (Kalıcı oturum)
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };

            // 6) SignIn (CookieAuthenticationDefaults.AuthenticationScheme)
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            // Debug loglar
            Console.WriteLine($"✅ Kullanıcı giriş yaptı: {user.UserName}");
            var userClaims = HttpContext.User.Claims.ToList();
            Console.WriteLine($"✅ Claims Count (Login Sonrası): {userClaims.Count}");
            foreach (var claim in userClaims)
                Console.WriteLine($"   - {claim.Type} : {claim.Value}");

            // Giriş sonrası test sayfasına yönlendirelim
            return RedirectToAction("ClaimsCheck", "Test");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        /// <summary>
        /// Kullanıcıya tanımlı Permission Claimlerini veritabanından alır.
        /// Örnek olarak: UserManager üzerinden eklenmiş "Permission" type Claimler
        /// </summary>
        private async Task<List<string>> GetUserPermissionsAsync(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
        }
    }
}
