using MediatR;

namespace WebSiteAPI.Application.Features.Queries.Category.GetAllCategory
{
    public class GetAllCategoryQueryRequest:IRequest<GetAllCategoryQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}