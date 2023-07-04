using WebSiteAPI.Domain.Common;

namespace WebSiteAPI.Domain.Identity
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }

        public Cart()
        {
            CartProducts = new List<CartProduct>();
        }
    }
}