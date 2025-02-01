using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.DTOs.Category;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReadRepository _readRepository;
        private readonly ICategoryWriteRepository _writeRepository;

        public CategoryService(ICategoryWriteRepository writeRepository, ICategoryReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task CreateAsync(Category model)
        {
            await _writeRepository.AddAsync(model);
            await _writeRepository.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var category = await _readRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            _writeRepository.Remove(category);
            await _writeRepository.SaveAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _readRepository.GetAll().ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            var category = await _readRepository.GetByIdAsync(id);
            return category;
        }

        public async Task UpdateAsync(UpdateCategory model)
        {
            Category? category = await this.GetByIdAsync(model.Id);
           

            category.Name = model.Name;
            category.Description = model.Description;

            _writeRepository.Update(category);
            await _writeRepository.SaveAsync();
        }
    }
}
