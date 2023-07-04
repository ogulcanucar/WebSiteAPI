using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Common;

namespace WebSiteAPI.Domain
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Product> Products { get; set; }

        public Brand()
        {
            Products = new List<Product>();
        }
    }

}
