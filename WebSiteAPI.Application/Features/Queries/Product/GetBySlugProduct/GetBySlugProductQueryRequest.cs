using MediatR;

namespace WebSiteAPI.Application.Features.Queries.Product.GetBySlugProduct
{
    public class GetBySlugProductQueryRequest:IRequest<GetBySlugProductQueryResponse>
    {
        public string Slug { get; set; }
    }
}