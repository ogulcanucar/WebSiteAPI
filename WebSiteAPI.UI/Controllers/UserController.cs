using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebSiteAPI.Application.Features.Commands.AppUser.CreateUser;
using WebSiteAPI.Application.Features.Commands.AppUser.DeleteUser;
using WebSiteAPI.Application.Features.Commands.AppUser.LoginUser;
using WebSiteAPI.Application.Features.Queries.AppUser.GetAllUsers;

namespace WebSiteAPI.UI.Controllers
{
    public class UserController : Controller
    {
        readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return View(response);
        }
        public async Task<IActionResult> List(GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return View(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteUserCommandRequest deleteUserCommandRequest)
        {
            DeleteUserCommandResponse response = await _mediator.Send(deleteUserCommandRequest);
            return RedirectToAction("List", "User");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {

            if (string.IsNullOrEmpty(loginUserCommandRequest.Username) || string.IsNullOrEmpty(loginUserCommandRequest.Password))
            {
                ModelState.AddModelError("", "Kullanıcı adı ve şifre gereklidir.");
                return View();
            }

            var authResponse = await _mediator.Send(loginUserCommandRequest);

            if (authResponse == null || !authResponse.Succeeded)
            {
                // Hatalı kullanıcı adı / şifre
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View();
            }

            // Kullanıcı rolleri
            var roles = authResponse.Roles ?? new List<string>();

            //  Cookie için kullanıcı bilgilerini Claims olarak hazırla
            var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, authResponse.UserId?.ToString() ?? ""),
    new Claim(ClaimTypes.Name, authResponse.UserName ?? loginUserCommandRequest.Username)
};

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // 🔹 Cookie oluştur (giriş işlemi)
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });


            // 🔹 (İsteğe bağlı) Rol kontrolü
            // if (!roles.Contains("SuperAdmin"))
            //     return RedirectToAction("AccessDenied", "Error");

            // 🔹 Başarılı girişten sonra yönlendirme
            Console.WriteLine("Login sonrası roller: " + string.Join(", ", roles));
            return RedirectToAction("Add", "Product");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }


    }
}
