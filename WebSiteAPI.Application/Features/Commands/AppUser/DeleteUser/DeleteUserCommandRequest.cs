using MediatR;

namespace WebSiteAPI.Application.Features.Commands.AppUser.DeleteUser
{
    public class DeleteUserCommandRequest:IRequest<DeleteUserCommandResponse>
    {
        public Guid UserId { get; set; }  
    }
}