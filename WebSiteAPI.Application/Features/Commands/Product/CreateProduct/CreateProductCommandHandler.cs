using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Features.Commands.Product.CreateProduct;
using a = WebSiteAPI.Application.DTOs.Product;

namespace WebSiteAPI.Application.Features.Commands.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var productDto = new a.CreateProduct
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsActive = request.IsActive,
                Populer = request.Populer,
                Categories = request.Categories,
                Material= request.Material,
                Material2= request.Material2,
                Material3 = request.Material3
            };



            await _productService.CreateAsync(productDto);


            return new CreateProductCommandResponse
            {
                ProductId = await _productService.LastProduct()
            };

        }
    }
}
