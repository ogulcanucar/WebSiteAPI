using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.DTOs.Auth;
using WebSiteAPI.Domain.Entities.Identity;
using WebSiteAPI.Application.Abstractions.Service.Authorization;

namespace WebSiteAPI.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Kullanıcı adı ve şifre ile giriş yapar, kimlik doğrulama yapar ve Cookie Authentication ile oturum açar.
        /// </summary>
        public async Task<AuthResponse> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return null;

            // Kullanıcı rollerini al
            var roles = await _userManager.GetRolesAsync(user);

            // Kullanıcı bilgilerini Claims olarak oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // Kullanıcının rollerini Claims'e ekle
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Kullanıcı tarayıcıyı kapatsa bile oturum devam etsin
            };

            // Kullanıcıyı oturum açtır
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return new AuthResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = new List<string>(roles)
            };
        }

        /// <summary>
        /// Kullanıcıyı oturumdan çıkarır.
        /// </summary>
        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
