using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Application.Repositories.Slider;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Persistence.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderReadRepository _sliderReadRepository;
        private readonly ISliderWriteRepository _sliderWriteRepository;

        public SliderService(ISliderReadRepository sliderReadRepository, ISliderWriteRepository sliderWriteRepository)
        {
            _sliderReadRepository = sliderReadRepository;
            _sliderWriteRepository = sliderWriteRepository;
        }

        public async Task CreateAsync(Slider model)
        {
            await _sliderWriteRepository.AddAsync(model);
            await _sliderWriteRepository.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await _sliderWriteRepository.RemoveAsync(id);
            await _sliderWriteRepository.SaveAsync();
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            var sliders = await _sliderReadRepository.GetAll().ToListAsync();
            return sliders;
        }

        public async Task<Slider> GetByIdAsync(string id)
        {
            var slider = await _sliderReadRepository.GetByIdAsync(id);
            return slider;
        }

        public async Task UpdateAsync(Slider model)
        {
            Slider? s = await this.GetByIdAsync(model.Id.ToString());
            s.FileName = model.FileName;
            s.Description = model.Description;
            s.Path = model.Path;
            s.Link = model.Link;
            s.Title = model.Title;
            s.Showcase = model.Showcase;
            await _sliderWriteRepository.UpdateAsync(s);
            await _sliderWriteRepository.SaveAsync();
        }
    }
}
