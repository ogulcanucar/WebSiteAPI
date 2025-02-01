using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebSiteAPI.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public string? CreatedBy { get; set; }  // Kullanıcının Id'si tutulur
        public string? UpdatedBy { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedBy= Id.ToString();
            UpdatedBy = Id.ToString();
        }
    }
}
