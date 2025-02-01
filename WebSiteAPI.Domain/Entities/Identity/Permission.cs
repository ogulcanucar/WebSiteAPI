using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities.Identity
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } // Yetki Adı (Örn: "Ürün Ekleme", "Ürün Silme")
    }

}
