using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Domain
{
    public class ProductImageFile : File
    {
        public bool Showcase { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
