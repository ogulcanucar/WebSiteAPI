using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Populer { get; set; }
        public decimal Price { get; set; }
        public string? Material { get; set; }
        public string? Material2 { get; set; }
        public string? Material3 { get; set; }
        public string Slug { get; set; }



        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public bool IsActive { get; set; }

        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
       

        /// <Stok>
        /// 
        /// </Stok>

        //public Guid BrandId { get; set; }
        //public Brand Brand { get; set; }


        public Product()
        {
            //ProductImageFiles = new List<ProductImageFile>();
            //BasketItems = new List<BasketItem>();
        }
    }


}
