using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories.Invoice
{
    public class InvoiceReadRepository : ReadRepository<Domain.Entities.Invoice>, IInvoiceReadRepository
    {
        public InvoiceReadRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
