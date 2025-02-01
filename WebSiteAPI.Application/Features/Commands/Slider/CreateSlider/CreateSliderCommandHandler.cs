using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Commands.Slider.CreateSlider
{
    public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommandRequest, CreateSliderCommandResponse>
    {
        public Task<CreateSliderCommandResponse> Handle(CreateSliderCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
