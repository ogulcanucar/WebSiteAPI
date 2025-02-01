using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Storage;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Application.Repositories.ProductImage;

namespace WebSiteAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile
{
    public class UploadProductImageFileCommandHandler : IRequestHandler<UploadProductImageFileCommandRequest, UploadProductImageFileCommandResponse>
    {
        readonly IStorageService _storageService;

        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageFileCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }

        public async Task<UploadProductImageFileCommandResponse> Handle(UploadProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);


            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);


            await _productImageFileWriteRepository.AddRangeAsync(result.Select((r,index) => new Domain.Entities.ProductImageFile
            {
                Showcase = (index == 0),
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
