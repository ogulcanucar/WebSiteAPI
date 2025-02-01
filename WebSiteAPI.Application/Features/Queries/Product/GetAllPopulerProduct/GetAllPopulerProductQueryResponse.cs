using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Queries.Product.GetAllPopulerProduct
{
    public class GetAllPopulerProductQueryResponse
    {
        public object Products { get; set; }
        public int TotalProductsCount { get; set; }
    }
}
