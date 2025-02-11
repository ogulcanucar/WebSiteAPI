using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebSiteAPI.Presentation.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult ClaimsCheck()
        {
            var userClaims = HttpContext.User.Claims.ToList();
            // Debug log
            System.Console.WriteLine($"🔎 Kullanıcı Claims Sayısı: {userClaims.Count}");
            foreach (var claim in userClaims)
                System.Console.WriteLine($"🔎 Claim: {claim.Type} - {claim.Value}");

            if (userClaims.Count == 0)
            {
                return Content("🚨 Kullanıcı giriş yapmamış veya Claims verisi taşınmıyor!");
            }

            return Content($"✅ Kullanıcı giriş yaptı! {userClaims.Count} adet claim yüklendi.\n" +
                           $"İlk Claim: {userClaims.FirstOrDefault()?.Type} - {userClaims.FirstOrDefault()?.Value}");
        }
    }
}
