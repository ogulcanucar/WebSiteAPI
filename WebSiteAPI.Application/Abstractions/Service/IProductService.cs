using WebSiteAPI.Application.DTOs.Product;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Abstractions.Service
{
    public interface IProductService
    {
        Task CreateAsync(CreateProduct model);
        Task<string> LastProduct();
        public Task<List<Product>> GetAllAsync();
        public Task<Product> GetByIdAsync(string id);
        Task UpdateAsync(UpdateProduct model);
        Task DeleteProductImageFileAsync(string productId,string imageId);
        Task DeleteAsync(string productId);
        Task<List<Product>> GetByCategoryIdProduct(string categoryId);
        Task<List<Product>> GetPopulerAllProduct();
        Task<Product> GetBySlugProduct(string slug);
    }
}
