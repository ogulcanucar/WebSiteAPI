using MediatR;
using Microsoft.AspNetCore.Http;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Features.Commands.Invoice.CreateInvoice
{
    public class CreateInvoiceCommandRequest:IRequest<CreateInvoiceCommandResponse>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}