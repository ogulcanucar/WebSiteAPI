using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebSiteAPI.Domain.Identity
{


    public class AppUser : IdentityUser<string>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        // İlişkiler
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public AppUser()
        {
            Carts = new List<Cart>();
            Orders = new List<Order>();
            Addresses = new List<Address>();
            Payments = new List<Payment>();
        }
    }


}
