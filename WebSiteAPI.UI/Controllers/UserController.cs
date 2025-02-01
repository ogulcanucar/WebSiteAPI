using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            return RedirectToAction("List","User");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
          LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return View(response);
        }
    }
}
