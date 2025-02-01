using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories.Slider;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories
{
    public class SliderReadRepository : ReadRepository<Slider>, ISliderReadRepository
    {
        public SliderReadRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
