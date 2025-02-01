using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Repositories.Slider;

namespace WebSiteAPI.Application.Features.Queries.Slider.GetAllSlider
{
    public class GetAllSliderQueryHandler : IRequestHandler<GetAllSliderQueryRequest, GetAllSliderQueryResponse>
    {
        readonly ISliderService _sliderService;

        public GetAllSliderQueryHandler(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<GetAllSliderQueryResponse> Handle(GetAllSliderQueryRequest request, CancellationToken cancellationToken)
        {
            var sliders = await _sliderService.GetAllAsync();
            return new()
            { Sliders = sliders };
        }
    }
}
