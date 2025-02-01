using Microsoft.EntityFrameworkCore;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.DTOs.Product;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Application.Repositories.Product;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Infrastructure.Operations;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Services
{
    public class ProductService : IProductService
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly ICategoryReadRepository _categoryReadRepository;
        private readonly WebSiteAPIContext _context;
        readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostEnvironment;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ICategoryService categoryService, ICategoryReadRepository categoryReadRepository, WebSiteAPIContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnvironment)
        {
            this._productReadRepository = productReadRepository;
            this._productWriteRepository = productWriteRepository;
            _categoryReadRepository = categoryReadRepository;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task CreateAsync(CreateProduct model)
        {
            SlugOperation slugOperation = new SlugOperation(_productReadRepository);
            model.Slug = slugOperation.ProductNameToNormalizeUniqueSlug(model.Name);

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                IsActive = model.IsActive,
                Populer = model.Populer,
                Material = model.Material,
                Material2 = model.Material2,
                Material3 = model.Material3,
                Slug = model.Slug
            };
            foreach (var catList in model.Categories)
            {
                var category = await _categoryReadRepository.GetByIdAsync(catList);

                if (category != null)
                {
                    var productCategory = new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = category.Id,
                        Product = product,
                        Category = category

                    };

                    product.ProductCategories.Add(productCategory);
                }


                await _productWriteRepository.AddAsync(product);
            }

            await _productWriteRepository.SaveAsync();

        }


        public async Task<List<Product>> GetAllAsync()
        {
            var product = await _productReadRepository.GetAll(false)
                .OrderBy(p => p.CreatedDate)
                .Include(p => p.ProductImageFiles)
                .Include(c => c.ProductCategories)
                .ToListAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            if (product != null)
            {
                await _context.Entry(product)
                        .Collection(p => p.ProductCategories)
                        .LoadAsync();
                await _context.Entry(product)
                      .Collection(p => p.ProductImageFiles)
                      .LoadAsync();
            }

            return product;
        }

        public async Task<string> LastProduct()
        {
            var product = _productReadRepository.Table.OrderByDescending(p => p.CreatedDate).ToList().FirstOrDefault();
            string Id = product.Id.ToString();
            return Id;
        }

        public async Task UpdateAsync(UpdateProduct model)
        {
            Product? product = await this.GetByIdAsync(model.Id.ToString());


            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.Material = model.Material;
                product.Material2 = model.Material2;
                product.Material3 = model.Material3;
                product.ProductCategories.Clear();
                await _productWriteRepository.UpdateAsync(product);

                if (model.Categories != null)
                {
                    foreach (var catList in model.Categories)
                    {
                        var category = await _categoryReadRepository.GetByIdAsync(catList);
                        if (category != null)
                        {
                            ProductCategory productCategory = new ProductCategory
                            {
                                ProductId = product.Id,
                                CategoryId = category.Id,
                                Product = product,
                                Category = category

                            };

                            await _context.AddAsync(productCategory);
                        }

                    }
                }

                await _productWriteRepository.SaveAsync();

            }
            else
                return;
        }

        public async Task DeleteProductImageFileAsync(string productId, string imageId)
        {
            var product = await this.GetByIdAsync(productId);
            if (product != null)
            {
                ProductImageFile? productImage = product.ProductImageFiles.Where(p => p.Id == Guid.Parse(imageId)).FirstOrDefault();
                if (productImage != null)
                {
                    Domain.Entities.File? file = await _context.Files.Where(p => p.Id == Guid.Parse(imageId)).FirstOrDefaultAsync();
                    if (file != null)
                    {
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, file.Path);
                        if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath); // Dosyayı sil
                        }
                        _context.Remove(file);
                        await _productWriteRepository.SaveAsync();
                    }
                }
            }
        }

        public async Task DeleteAsync(string productId)
        {
            var product = await this.GetByIdAsync(productId);
            ProductImageFile productImageFile = new();

            foreach (var item in product.ProductImageFiles)
            {
                await this.DeleteProductImageFileAsync(productId, item.Id.ToString());
            }
            await _productWriteRepository.RemoveAsync(productId);
            await _productWriteRepository.SaveAsync();

        }

        public async Task<List<Product>> GetByCategoryIdProduct(string categoryId)
        {

            var product = await _productReadRepository.GetAll(false)
           .Include(p => p.ProductImageFiles)
           .Include(c => c.ProductCategories)
           .Where(p => p.ProductCategories.Any(c => c.Category.Name == categoryId))
           .ToListAsync();
            return product;
        }
        public async Task<List<Product>> GetPopulerAllProduct()
        {
            var product = await _productReadRepository.GetAll(false)
                .Include(pi => pi.ProductImageFiles)
                .Include(c => c.ProductCategories)
                .Where(p => p.Populer == true)
                .ToListAsync();
            return product;
        }

        public async Task<Product> GetBySlugProduct(string slug)
        {
            var product = await _productReadRepository.Table.FirstOrDefaultAsync(a => a.Slug == slug);
            if (product != null)
            {
                await _context.Entry(product)
                        .Collection(p => p.ProductCategories)
                        .Query()
                        .Include(pc=>pc.Category)
                        .LoadAsync();
                await _context.Entry(product)
                      .Collection(p => p.ProductImageFiles)
                      .LoadAsync();
            }

            return product;
        }
    }
}
