using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.Application.Features.Commands.AppUser.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
    {
        readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(request.UserId);
            return new();

        }
    }
}
