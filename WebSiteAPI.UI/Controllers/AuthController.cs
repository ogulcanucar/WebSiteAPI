using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebSiteAPI.Application.Features.Commands.Auth.Login;

namespace WebSiteAPI.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
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

            // **Claims: Kullanıcı bilgilerini taşıyan yapıdır.**
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()), // Kullanıcının GUID ID'si
                new Claim(ClaimTypes.Name, result.UserName) // Kullanıcının Adı
            };

            // **Kullanıcının Rollerini Claims'e ekleyelim**
            foreach (var role in result.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Kullanıcının sahip olduğu roller
            }

            // **Claims Identity: Kullanıcının kimliğini oluşturur.**
            var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);

            // **AuthenticationProperties: Oturumun kalıcı olup olmadığını belirler.**
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Kullanıcı tarayıcıyı kapatsa bile oturum devam etsin
                ExpiresUtc = DateTime.UtcNow.AddDays(7) // 7 gün boyunca giriş açık kalsın
            };

            // **Kullanıcıyı oturum açtır**
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            Console.WriteLine("SignInAsync Başarılı! Kullanıcı: " + result.UserName);

            // Başarılı girişten sonra yönlendirme
            return RedirectToAction("add", "User");
        }

        /// <summary>
        /// Kullanıcıyı oturumdan çıkarır.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}
