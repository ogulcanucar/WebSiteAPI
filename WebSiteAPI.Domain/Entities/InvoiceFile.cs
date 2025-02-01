using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Domain.Entities
{
    public class InvoiceFile : File
    {
        public ICollection<Invoice> Invoices { get; set; }

    }
}
