using MediatR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Repositories;

namespace WebSiteAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductService _productService;
        readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductService productService, IProductReadRepository productReadRepository)
        {
            _productService = productService;
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllAsync();

            return new()
            {
                Products = products,
            };
        }
    }
}
