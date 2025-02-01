using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.UI.ViewComponents
{
    public class ProductDetailCategoryMenuViewComponent : ViewComponent
    {
        readonly ICategoryService _categoryService;

        public ProductDetailCategoryMenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
    }
}
