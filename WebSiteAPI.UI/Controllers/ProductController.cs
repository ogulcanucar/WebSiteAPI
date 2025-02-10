using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Design;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Features.Commands.Product.CreateProduct;
using WebSiteAPI.Application.Features.Commands.Product.DeleteProduct;
using WebSiteAPI.Application.Features.Commands.Product.UpdateProduct;
using WebSiteAPI.Application.Features.Commands.ProductImageFile.DeleteProductImageFile;
using WebSiteAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile;
using WebSiteAPI.Application.Features.Queries.Product.GetAllProduct;
using WebSiteAPI.Application.Features.Queries.Product.GetByCategoryIdProduct;
using WebSiteAPI.Application.Features.Queries.Product.GetByIdProduct;
using WebSiteAPI.Application.Features.Queries.Product.GetBySlugProduct;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Infrastructure.Operations;

namespace WebSiteAPI.UI.Controllers
{
    [Authorize(Policy = "ProductManage")]
    public class ProductController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductService _productService;

        public ProductController(IMediator mediator, ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository, IProductService productService)
        {
            _mediator = mediator;
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            var categories = _categoryReadRepository.GetAll();
            ViewBag.Categories = categories;

            // **Token'ın doğrulanıp doğrulanmadığını kontrol edelim**
            ViewBag.Authenticated = User.Identity.IsAuthenticated;
            ViewBag.UserRoles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();
            ViewBag.UserId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "Token doğrulanamadı!";
                return RedirectToAction("AccessDenied", "Error");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return RedirectToAction("Upload", new { Id = response.ProductId });
        }
        [HttpGet]
        public async Task<IActionResult> Upload(string Id)
        {
            Product? product = await _productService.GetByIdAsync(Id);
            if (product != null)
                return View(product);
            else
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(UploadProductImageFileCommandRequest uploadProductImageFileCommandRequest)
        {
            UploadProductImageFileCommandResponse response = await _mediator.Send(uploadProductImageFileCommandRequest);
            return RedirectToAction("List");

        }
        [HttpGet]
        public async Task<IActionResult> List(GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Update(GetByIdProductQueryRequest getByIdProductQueryRequest, string Id)
        {
            var categories = _categoryReadRepository.GetAll(); // Kategori verilerini alın, bu kısım sizin uygulamanıza bağlıdır
            ViewBag.Categories = categories;
            getByIdProductQueryRequest.ProductId = Id;
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> DeleteImage(string productId, string imageId, DeleteProductImageFileRequest request)
        {
            DeleteProductImageFileResponse response = await _mediator.Send(request);
            request.ProductId = productId;
            request.ImageId = imageId;
            return RedirectToAction("Upload", new { Id = productId });

        }

        public async Task<IActionResult> Delete(string productId, DeleteProductCommandRequest request)
        {
            request.ProductId = productId;
            DeleteProductCommandResponse response = await _mediator.Send(request);

            return RedirectToAction("list");
        }

        public async Task<IActionResult> ProductList(string catName, GetByCategoryIdProductQueryRequest request)
        {

            request.CatId = UrlHelperOperation.RestoreCategoryFromUrl(catName);
            ViewData["Page1"] = "Ürünler";
            ViewData["Page2"] = request.CatId;
            ViewData["Page2Url"] = "/" + catName;
            GetByCategoryIdProductQueryResponse response = await _mediator.Send(request);
            return View(response);
        }
        public async Task<IActionResult> AllProductList(GetAllProductQueryRequest request)
        {

            ViewData["Page1"] = "Ürünler";
            GetAllProductQueryResponse response = await _mediator.Send(request);
            return View(response);
        }
        public async Task<IActionResult> ProductDetail(string pName, GetBySlugProductQueryRequest request)
        {
            request.Slug = pName;
            GetBySlugProductQueryResponse response = await _mediator.Send(request);
            return View(response);
        }

    }
}
