using WebSiteAPI.Domain.Common;

namespace WebSiteAPI.Domain.Identity
{
    public class Address:BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}