using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public Category()
        {
            ProductCategories = new List<ProductCategory>();
        }

    }
}
