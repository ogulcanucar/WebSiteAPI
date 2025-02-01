using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.Application.Features.Queries.Category.GetByIdCategory
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQueryRequest, GetByIdCategoryQueryResponse>
    {readonly ICategoryService _categoryService;

        public GetByIdCategoryQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<GetByIdCategoryQueryResponse> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIdAsync(request.Id);
            return new() { Categories= category };
        }
    }
}
