using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebSiteAPI.Application.Features.Commands.Mail.CreateMail;

namespace WebSiteAPI.UI.Controllers
{
    public class ContactController : Controller
    {
        readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateMailCommandRequest request)
        {
            CreateMailCommandResponse response = await _mediator.Send(request);
            return View(response);
        }
    }
}
