using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.DTOs.Category;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Abstractions.Service
{
    public interface ISliderService
    {
        public Task CreateAsync(Slider model);
        public Task<List<Slider>> GetAllAsync();
        public Task DeleteAsync(string id);
        public Task UpdateAsync(Slider model);
        public Task<Slider> GetByIdAsync(string id);
    }
}
