using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQuery : IRequest<List<string>> { }

}
