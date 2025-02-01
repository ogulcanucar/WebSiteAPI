using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.Application.Features.Commands.Mail.CreateMail
{
    public class CreateMailCommandHandler : IRequestHandler<CreateMailCommandRequest, CreateMailCommandResponse>
    {
        readonly IMailService _mailService;

        public CreateMailCommandHandler(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task<CreateMailCommandResponse> Handle(CreateMailCommandRequest request, CancellationToken cancellationToken)
        {
            await _mailService.CreateAsync(new()
            {
                Body = request.Body,
                Email = request.Email,
                Subject = request.Subject,
                Phone = request.Phone,
                Surname = request.Surname,
                Name = request.Name,

            });
            return new();

        }
    }
}
