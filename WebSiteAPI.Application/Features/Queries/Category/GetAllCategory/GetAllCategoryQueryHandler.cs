using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Application.Features.Queries.Category.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryResponse>
    {
        readonly ICategoryService _categoryService;

        public GetAllCategoryQueryHandler(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public async Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetAllAsync();
           
            return new()
            {
                Categories = category
            };
        }
    }
}
