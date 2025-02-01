using MediatR;
using Microsoft.AspNetCore.Http;

namespace WebSiteAPI.Application.Features.Commands.Slider.CreateSlider
{
    public class CreateSliderCommandRequest:IRequest<CreateSliderCommandResponse>
    {
        public bool Showcase { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}