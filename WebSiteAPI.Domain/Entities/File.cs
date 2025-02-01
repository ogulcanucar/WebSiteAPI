using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities
{
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
        [NotMapped]
        public virtual DateTime UpdatedDate { get; set; }



    }
}
