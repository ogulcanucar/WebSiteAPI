using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Repositories;

namespace WebSiteAPI.Application.Features.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
    {
        readonly ICategoryWriteRepository _repository;

        public CreateCategoryCommandHandler(ICategoryWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new()
            {
                Name = request.Name,
                Description= request.Description,
            });
            await _repository.SaveAsync();
            return new();
        }
    }
}
