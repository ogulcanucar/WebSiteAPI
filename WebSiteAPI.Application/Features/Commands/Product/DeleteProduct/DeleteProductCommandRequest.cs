using MediatR;

namespace WebSiteAPI.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandRequest:IRequest<DeleteProductCommandResponse>
    {
        public string ProductId { get; set; }
    }
}