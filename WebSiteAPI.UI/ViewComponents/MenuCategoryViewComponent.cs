using Microsoft.AspNetCore.Mvc;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.UI.ViewComponents
{
    public class MenuCategoryViewComponent: ViewComponent
    {readonly ICategoryService _categoryService;

        public MenuCategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories =await _categoryService.GetAllAsync();
            return View(categories);
        }
       
    }
}
