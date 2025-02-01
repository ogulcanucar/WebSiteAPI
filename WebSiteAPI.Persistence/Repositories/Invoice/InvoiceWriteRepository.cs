using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories
{
    public class InvoiceWriteRepository : WriteRepository<Domain.Entities.Invoice>, IInvoiceWriteRepository
    {
        public InvoiceWriteRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
