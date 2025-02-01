using WebSiteAPI.Application.DTOs.Category;
using WebSiteAPI.Domain.Entities;
//using WebSiteAPI.Application.DTOs.Category;

namespace WebSiteAPI.Application.Abstractions.Service
{
    public interface ICategoryService
    {
        public Task CreateAsync(Category model);
        public Task<List<Category>> GetAllAsync();
        public Task DeleteAsync(string id);
        public Task UpdateAsync(UpdateCategory model);
        Task<Category> GetByIdAsync(string id);

    }
}
