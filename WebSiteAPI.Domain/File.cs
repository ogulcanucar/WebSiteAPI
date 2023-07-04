using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Common;

namespace WebSiteAPI.Domain
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }

    }
}
