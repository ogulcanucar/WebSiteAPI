using WebSiteAPI.Domain.Entities.Common;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}