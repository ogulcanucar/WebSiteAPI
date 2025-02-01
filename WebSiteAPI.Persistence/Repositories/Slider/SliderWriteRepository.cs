using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories
{
    public class SliderWriteRepository : WriteRepository<Slider>, ISliderWriteRepository
    {
        public SliderWriteRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
