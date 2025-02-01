using MediatR;

namespace WebSiteAPI.Application.Features.Commands.ProductImageFile.DeleteProductImageFile
{
    public class DeleteProductImageFileRequest:IRequest<DeleteProductImageFileResponse>
    {
        public string ProductId { get; set; }
        public string ImageId { get; set; }
    }
}