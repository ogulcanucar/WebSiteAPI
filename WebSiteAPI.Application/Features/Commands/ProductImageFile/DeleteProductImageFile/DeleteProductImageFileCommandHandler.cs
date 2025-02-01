using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Repositories.Product;
using a = WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Features.Commands.ProductImageFile.DeleteProductImageFile
{
    public class DeleteProductImageFileCommandHandler : IRequestHandler<DeleteProductImageFileRequest, DeleteProductImageFileResponse>
    {
        readonly IProductService _productService;


        public DeleteProductImageFileCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<DeleteProductImageFileResponse> Handle(DeleteProductImageFileRequest request, CancellationToken cancellationToken)
        {
            await _productService.DeleteProductImageFileAsync(request.ProductId, request.ImageId);
            return new();
        }
    }
}
