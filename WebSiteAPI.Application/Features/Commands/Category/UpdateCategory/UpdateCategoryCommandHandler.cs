using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.DTOs.Category;
using a=WebSiteAPI.Application.DTOs.Category;

namespace WebSiteAPI.Application.Features.Commands.Category.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
    {readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {

            var cat = new a.UpdateCategory
            {
                Id=request.Id,
                Name = request.Name,
                Description = request.Description,

            };
            await _categoryService.UpdateAsync(cat);
            return new();
        }
    }
}
