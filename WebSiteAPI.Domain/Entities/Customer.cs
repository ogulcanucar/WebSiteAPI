using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Note { get; set; }
        public string? PersonName { get; set; }
        public string? Iban { get; set; }


    }
}
