using MediatR;

namespace WebSiteAPI.Application.Features.Commands.Mail.CreateMail
{
    public class CreateMailCommandRequest:IRequest<CreateMailCommandResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}