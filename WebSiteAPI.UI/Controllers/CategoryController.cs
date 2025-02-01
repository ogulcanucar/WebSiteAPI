using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using WebSiteAPI.Application.Features.Commands.Category.CreateCategory;
using WebSiteAPI.Application.Features.Commands.Category.DeleteCategory;
using WebSiteAPI.Application.Features.Commands.Category.UpdateCategory;
using WebSiteAPI.Application.Features.Queries.Category.GetAllCategory;
using WebSiteAPI.Application.Features.Queries.Category.GetByIdCategory;

namespace WebSiteAPI.UI.Controllers
{
    public class CategoryController : Controller
    {
        readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            CreateCategoryCommandResponse response = await _mediator.Send(createCategoryCommandRequest);
            return View(response);
        }
        public async Task<IActionResult> List(GetAllCategoryQueryRequest listCategoryQueryRequest)
        {
            GetAllCategoryQueryResponse response = await _mediator.Send(listCategoryQueryRequest);
            return View(response);
        }
        public async Task<IActionResult> Update(GetByIdCategoryQueryRequest request, string Id)
        {
            request.Id = Id;
            GetByIdCategoryQueryResponse response = await _mediator.Send(request);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryCommandRequest request)
        {
            UpdateCategoryCommandResponse response = await _mediator.Send(request);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Delete(DeleteCategoryCommandRequest request,string Id)
        {
            request.Id = Id;
            DeleteCategoryCommandResponse response= await _mediator.Send(request);
            return RedirectToAction("List");
        }
    }
}
