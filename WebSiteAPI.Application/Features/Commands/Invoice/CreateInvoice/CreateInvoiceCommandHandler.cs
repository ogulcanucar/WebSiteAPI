using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Features.Commands.Invoice.CreateInvoice;
using a = WebSiteAPI.Application.DTOs.Invoice;

namespace WebSiteAPI.Application.Features.Commands.Invoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommandRequest, CreateInvoiceCommandResponse>
    {
        readonly IInvoiceService _invoiceService;

        public CreateInvoiceCommandHandler(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<CreateInvoiceCommandResponse> Handle(CreateInvoiceCommandRequest request, CancellationToken cancellationToken)
        {
            var invoiceDto = new a.CreateInvoice()
            {
                Files = request.Files,
                Name = request.Name,
                Price = request.Price,
                Type = request.Type,
            };
            await _invoiceService.CreateAsync(invoiceDto);
            return new();
        }
    }
}
