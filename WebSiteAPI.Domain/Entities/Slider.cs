using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities
{
    public class Slider:File
    {
        public bool Showcase { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
