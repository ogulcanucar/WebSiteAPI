using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.DTOs.Auth;

namespace WebSiteAPI.Application.Features.Commands.Auth.Login
{
    /// <summary>
    /// Kullanıcı giriş işlemi için MediatR Command sınıfı
    /// </summary>
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
