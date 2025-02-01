using MediatR;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

       

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.AuthenticateAsync(request.Username, request.Password);

            return new LoginUserCommandResponse
            {
            //    Succeeded = result.Succeeded,
            //    Message = result.Succeeded ? "Giriş Başarılı" : "Hatalı Şifre veya Parola"
            };
        }
    }
}