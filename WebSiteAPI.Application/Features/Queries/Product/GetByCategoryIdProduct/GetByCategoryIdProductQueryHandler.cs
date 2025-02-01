using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.Application.Features.Queries.Product.GetByCategoryIdProduct
{
    public class GetByCategoryIdProductQueryHandler : IRequestHandler<GetByCategoryIdProductQueryRequest, GetByCategoryIdProductQueryResponse>
    {
        readonly IProductService _productService;

        public GetByCategoryIdProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetByCategoryIdProductQueryResponse> Handle(GetByCategoryIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByCategoryIdProduct(request.CatId);
            return new() { Products = product };
        }
    }
}
