using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Persistence.Contexts;
using a=WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Persistence.Repositories
{
    public class InvoiceFileWriteRepository : WriteRepository<a.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
