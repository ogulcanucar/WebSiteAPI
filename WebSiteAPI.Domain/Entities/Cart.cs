
using WebSiteAPI.Domain.Entities.Common;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Cart()
        {
            CartProducts = new List<CartProduct>();
        }
    }
}