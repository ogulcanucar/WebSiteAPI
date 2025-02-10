using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
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

        /// <summary>
        /// Kullanıcı giriş sayfasını döndürür.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Kullanıcı girişini kontrol eder ve oturum açar.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _mediator.Send(new LoginCommand(username, password));

            if (result == null)
            {
                ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
                return View();
            }

            // Kullanıcıyı veritabanından çekiyoruz
            var user = await _userManager.FindByIdAsync(result.UserId.ToString());

            if (user == null)
            {
                ViewBag.Error = "Kullanıcı bulunamadı!";
                return View();
            }

            // **📌 Kullanıcı Claims Bilgileri**
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ?? Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? "UnknownUser")
            };

            // **📌 Kullanıcının Rollerini Claims'e ekleyelim**
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                Console.WriteLine($"🟢 Kullanıcı Rolü Eklendi: {role}");

                // Eğer kullanıcı "Admin" rolündeyse "AdminAccess" yetkisini verelim
                if (role == "Admin")
                {
                    claims.Add(new Claim("Permission", "AdminAccess"));
                }
            }

            // **📌 Kullanıcının Yetkilerini (Permissions) Veritabanından Alalım**
            var userPermissions = await GetUserPermissionsAsync(user);
            foreach (var permission in userPermissions)
            {
                claims.Add(new Claim("Permission", permission));
                Console.WriteLine($"🟢 Kullanıcı Yetkisi Eklendi: {permission}");
            }

            // **📌 Kullanıcı Kimliği (ClaimsIdentity) Oluştur**
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // **📌 Oturum Açma Ayarları**
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };

            // **📌 Kullanıcıyı oturum açtır**
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            // **📌 Debug Loglar**
            Console.WriteLine($"✅ Kullanıcı giriş yaptı: {user.UserName}");
            Console.WriteLine($"✅ Çerez Verisi: {HttpContext.Request.Cookies[".AspNetCore.Cookies"]}");

            // **📌 Kullanıcı Claims'lerini kontrol edelim**
            var userClaims = HttpContext.User.Claims.ToList();
            Console.WriteLine($"✅ Kullanıcı Claims Sayısı: {userClaims.Count}");
            foreach (var claim in userClaims)
            {
                Console.WriteLine($"✅ Claim: {claim.Type} - {claim.Value}");
            }

            // 📌 Giriş yaptıktan sonra Claims Kontrol sayfasına yönlendir
            return RedirectToAction("ClaimsCheck", "Test");
        }

        /// <summary>
        /// Kullanıcıyı oturumdan çıkarır.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        /// <summary>
        /// Kullanıcının Yetkilerini (Claims) Veritabanından Alır
        /// </summary>
        private async Task<List<string>> GetUserPermissionsAsync(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
        }
    }
}
