using WebSiteAPI.Application.Repositories.ProductImage;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
