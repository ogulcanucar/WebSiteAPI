using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebSiteAPI.Presentation.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult ClaimsCheck()
        {
            var userClaims = HttpContext.User.Claims.ToList();
            Console.WriteLine($"🔎 Kullanıcı Claims Sayısı: {userClaims.Count}");

            foreach (var claim in userClaims)
            {
                Console.WriteLine($"🔎 Claim: {claim.Type} - {claim.Value}");
            }

            if (userClaims.Count == 0)
            {
                return Content("🚨 Kullanıcı giriş yapmamış veya Claims verisi taşınmıyor!");
            }

            return Content($"✅ Kullanıcı giriş yaptı! {userClaims.Count} adet claim yüklendi.");
        }
    }
}
