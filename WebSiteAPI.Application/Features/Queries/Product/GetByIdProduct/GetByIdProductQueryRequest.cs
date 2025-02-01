using MediatR;

namespace WebSiteAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
    {
        public string ProductId { get; set; }
    }
}