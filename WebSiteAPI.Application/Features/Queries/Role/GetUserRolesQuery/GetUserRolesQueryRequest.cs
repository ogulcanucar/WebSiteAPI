using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Queries.Role.GetUserRolesQuery
{
    public class GetUserRolesQueryRequest : IRequest<List<string>>
    {
        public string UserId { get; set; }
    }
}
