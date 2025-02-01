using MediatR;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.DTOs.User;

namespace WebSiteAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Username = request.Username,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
            });
            return new() { Message = response.Message, Succeeded = response.Succeeded };
        }
    }
}
