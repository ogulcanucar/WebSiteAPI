using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.Application.Features.Queries.Product.GetBySlugProduct
{
    public class GetBySlugProductQueryHandler : IRequestHandler<GetBySlugProductQueryRequest, GetBySlugProductQueryResponse>
    {
        readonly IProductService _productService;

        public GetBySlugProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetBySlugProductQueryResponse> Handle(GetBySlugProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetBySlugProduct(request.Slug);
            return new() { Product = product };
        }
    }
}
