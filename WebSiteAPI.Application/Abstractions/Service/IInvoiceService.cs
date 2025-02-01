using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.DTOs.Invoice;

namespace WebSiteAPI.Application.Abstractions.Service
{
    public interface IInvoiceService
    {
        Task CreateAsync(CreateInvoice model);
    }
}
