using MediatR;

namespace WebSiteAPI.Application.Features.Commands.Category.UpdateCategory
{
    public class UpdateCategoryCommandRequest:IRequest<UpdateCategoryCommandResponse>
    {
        public string Id { get; set;}
        public string Name { get; set;}
        public string Description { get; set;}
    }
}