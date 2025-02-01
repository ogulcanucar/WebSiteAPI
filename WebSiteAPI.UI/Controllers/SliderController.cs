using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Application.Repositories.Slider;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Persistence.Repositories;
using WebSiteAPI.Persistence.Services;

namespace WebSiteAPI.UI.Controllers
{
    public class SliderController : Controller
    {
        readonly IMediator _meditor;

        public SliderController(IMediator meditor)
        {
            _meditor = meditor;
        }

        public async Task<IActionResult> List()
        {
           // var slider = await _sliderService.GetAllAsync();
           //if(slider!=null)
            return Ok();                           
        }
    }
}
