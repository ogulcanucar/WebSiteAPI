using MediatR;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.DTOs.Auth;

namespace WebSiteAPI.Application.Features.Commands.Auth.Login
{
    /// <summary>
    /// Kullanıcı giriş işlemini yöneten MediatR Handler sınıfı
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Kullanıcı giriş isteğini işler ve AuthService üzerinden kimlik doğrulama yapar.
        /// </summary>
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Username, request.Password);
        }
    }
}
