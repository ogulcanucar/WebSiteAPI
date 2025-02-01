using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.DTOs.Invoice
{
    public class CreateInvoice
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public IFormFileCollection? Files { get; set; }

    }
}
