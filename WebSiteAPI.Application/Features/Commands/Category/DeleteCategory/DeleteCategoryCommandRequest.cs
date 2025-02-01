using MediatR;

namespace WebSiteAPI.Application.Features.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommandRequest : IRequest<DeleteCategoryCommandResponse>
    {
        public string Id { get; set; }
    }
}