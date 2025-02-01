namespace WebSiteAPI.Application.Features.Commands.Auth.Login
{
    public class LoginCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Token { get; set; } // Kullanıcı başarılı giriş yaptıysa JWT Token döneceğiz
    }

}