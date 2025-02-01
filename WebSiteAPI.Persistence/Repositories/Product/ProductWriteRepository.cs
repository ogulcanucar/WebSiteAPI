using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories.Product;
using WebSiteAPI.Persistence.Contexts;

namespace WebSiteAPI.Persistence.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(WebSiteAPIContext context) : base(context)
        {
        }
    }
}
