using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Features.Queries.Product.GetAllProduct;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Features.Queries.Product.GetAllPopulerProduct
{
    public class GetAllPopulerProductQueryHandler :IRequestHandler<GetAllPopulerProductQueryRequest,GetAllPopulerProductQueryResponse>
    {
        readonly IProductService _productService;
        readonly IProductReadRepository _productReadRepository;
        public GetAllPopulerProductQueryHandler(IProductService productService, IProductReadRepository productReadRepository)
        {
            _productService = productService;
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllPopulerProductQueryResponse> Handle(GetAllPopulerProductQueryRequest request, CancellationToken cancellationToken)
        {
            var populerProducts = await _productService.GetPopulerAllProduct();
            return new()
            {
                Products = populerProducts,
            };
        }

       
    }
}
