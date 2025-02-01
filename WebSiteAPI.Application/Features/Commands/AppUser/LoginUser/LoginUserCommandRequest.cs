using MediatR;

namespace WebSiteAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}