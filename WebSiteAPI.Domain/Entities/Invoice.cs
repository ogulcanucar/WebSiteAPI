using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities
{
    public class Invoice:BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public ICollection<InvoiceFile> InvoiceFiles { get; set; }

    }

}
