using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebSiteAPI.Application.Features.Commands.Invoice.CreateInvoice;

namespace WebSiteAPI.UI.Controllers
{
    public class InvoiceController : Controller
    {
        readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateInvoiceCommandRequest createInvoiceCommandRequest)
        {
            CreateInvoiceCommandResponse response=await _mediator.Send(createInvoiceCommandRequest);
            return RedirectToAction("Home");
        }
    }
}
