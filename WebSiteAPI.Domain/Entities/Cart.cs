
using WebSiteAPI.Domain.Entities.Common;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid? OrderId { get; set; } // ✅ Nullable yapıldı
        public Order? Order { get; set; }  // ✅ Nullable yapıldı (opsiyonel ilişki)

      
    }
}