using System.Threading.Tasks;
using WebSiteAPI.Application.DTOs.Auth;

namespace WebSiteAPI.Application.Abstractions.Service.Authorization
{
    public interface IAuthService
    {
        /// <summary>
        /// Kullanıcı adı ve şifre ile giriş yapar, kimlik doğrulama yapar ve Cookie Authentication ile oturum açar.
        /// </summary>
        Task<AuthResponse> LoginAsync(string username, string password);

        /// <summary>
        /// Kullanıcıyı oturumdan çıkarır.
        /// </summary>
        Task LogoutAsync();
    }
}
