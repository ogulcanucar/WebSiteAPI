using WebSiteAPI.Domain.Entities.Common;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Domain.Entities
{
    
        public class Order : BaseEntity
        {
            public Guid UserId { get; set; }
            public AppUser User { get; set; }

            public Cart? Cart { get; set; } // ✅ Opsiyonel yapıldı
        }

    }
