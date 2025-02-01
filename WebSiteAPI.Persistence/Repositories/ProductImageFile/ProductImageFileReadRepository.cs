using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories.ProductImage;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories.ProductImage
{
    public class ProductImageFileReadRepository : ReadRepository<Domain.Entities.ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
