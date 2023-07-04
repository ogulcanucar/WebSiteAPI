using WebSiteAPI.Domain.Common;

namespace WebSiteAPI.Domain.Identity
{
    public class CartProduct : BaseEntity
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}