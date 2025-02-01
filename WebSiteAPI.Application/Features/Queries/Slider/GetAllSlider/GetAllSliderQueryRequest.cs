using MediatR;

namespace WebSiteAPI.Application.Features.Queries.Slider.GetAllSlider
{
    public class GetAllSliderQueryRequest:IRequest<GetAllSliderQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}