using MediatR;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using a = WebSiteAPI.Application.DTOs.Product;

namespace WebSiteAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
         readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var productDto = new a.UpdateProduct()
            {
                Id = request.Id,
                Name = request.Name,
                Categories = request.Categories,
                Description = request.Description,
                IsActive = request.IsActive,
                Price = request.Price
            };
            await _productService.UpdateAsync(productDto);
            return new();

        }
    }
}
