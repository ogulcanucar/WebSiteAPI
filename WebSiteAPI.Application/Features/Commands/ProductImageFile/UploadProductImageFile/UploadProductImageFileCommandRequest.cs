using MediatR;
using Microsoft.AspNetCore.Http;

namespace WebSiteAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile
{
    public class UploadProductImageFileCommandRequest : IRequest<UploadProductImageFileCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}