using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;

namespace WebSiteAPI.Application.Features.Queries.Role.GetUserRolesQuery
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQueryRequest, List<string>>
    {
        private readonly IUserService _userService;

        public GetUserRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<string>> Handle(GetUserRolesQueryRequest request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserRolesAsync(request.UserId);
        }
    }

}
