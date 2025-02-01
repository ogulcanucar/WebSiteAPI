using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSiteAPI.Application.Features.Queries.Product.GetAllPopulerProduct;
using WebSiteAPI.Application.Features.Queries.Product.GetAllProduct;
using WebSiteAPI.UI.Models;

namespace WebSiteAPI.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IActionResult> Index(GetAllPopulerProductQueryRequest request)
        {
            GetAllPopulerProductQueryResponse response = await _mediator.Send(request);
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}