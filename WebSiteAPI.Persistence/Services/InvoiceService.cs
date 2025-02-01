using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Abstractions.Storage;
using WebSiteAPI.Application.DTOs.Invoice;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Persistence.Services
{
    public class InvoiceService : IInvoiceService
    {
        readonly IInvoiceWriteRepository _invoiceWriteRepository;
        readonly IInvoiceReadRepository _invoiceReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IStorageService _storageService;
        public InvoiceService(IInvoiceReadRepository invoiceReadRepository, IInvoiceWriteRepository invoiceWriteRepository, IStorageService storageService, IInvoiceFileWriteRepository invoiceFileWriteRepository)
        {
            _invoiceReadRepository = invoiceReadRepository;
            _invoiceWriteRepository = invoiceWriteRepository;
            _storageService = storageService;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
        }

        public async Task CreateAsync(CreateInvoice model)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("Invoice-File", model.Files);

            var invoice = new Invoice
            {
                Name = model.Name,
                Price = model.Price,
                Type=model.Type,

            };
          
                await _invoiceFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.InvoiceFile
                {
                    FileName = r.fileName,
                    Path = r.pathOrContainerName,
                    Storage = _storageService.StorageName,
                    Invoices = new List<Domain.Entities.Invoice>() { invoice }
                }).ToList());

            
            await _invoiceWriteRepository.AddAsync(invoice);
            await _invoiceWriteRepository.SaveAsync();

        }
    }
}
