using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Common;

namespace WebSiteAPI.Domain
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public bool IsActive { get; set; }

        //public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        //public ICollection<BasketItem> BasketItems { get; set; }

        /// <Stok>
        public IDictionary<string, int> StockByColor { get; set; }
        /// 
        /// </Stok>

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }


        public Product()
        {
            StockByColor = new Dictionary<string, int>();
            //ProductImageFiles = new List<ProductImageFile>();
            //BasketItems = new List<BasketItem>();
        }
    }


}
