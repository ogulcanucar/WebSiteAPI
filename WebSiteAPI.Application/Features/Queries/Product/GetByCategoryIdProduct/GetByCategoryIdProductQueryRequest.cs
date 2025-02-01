using MediatR;

namespace WebSiteAPI.Application.Features.Queries.Product.GetByCategoryIdProduct
{
    public class GetByCategoryIdProductQueryRequest:IRequest<GetByCategoryIdProductQueryResponse>
    {
        public string CatId { get; set; }
    }
}