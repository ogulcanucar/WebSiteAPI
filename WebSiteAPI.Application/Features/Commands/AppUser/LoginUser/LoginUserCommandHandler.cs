using MediatR;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Abstractions.Service.Authorization;

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
            // AuthenticateAsync metodunuz artık AuthResponse dönüyor olmalı (UserId, UserName, Roles)
            var authResult = await _userService.AuthenticateAsync(request.Username, request.Password);

            if (authResult == null)
            {
                return new LoginUserCommandResponse
                {
                    Succeeded = false,
                    Message = "Kullanıcı adı veya şifre hatalı",
                    Roles = new List<string>()
                };
            }

            return new LoginUserCommandResponse
            {
                Succeeded = true,
                Message = "Giriş başarılı",
                Roles = authResult.Roles,
                UserId = authResult.UserId.ToString(),  // <-- Burayı kesinleştir
                UserName = authResult.UserName
            };

        }
    }
}