using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.DTOs.Product
{
    public class CreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool Populer { get; set; }
        public List<string> Categories { get; set; }
        public string? Material { get; set; }
        public string? Material2 { get; set; }
        public string? Material3 { get; set; }
        public string Slug { get; set; }
    }


}
