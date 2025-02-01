using MediatR;
using System;
using WebSiteAPI.Application.Features.Queries.AppRole.GetRoleById;
namespace WebSiteAPI.Application.Features.Queries.AppRole.GetRoleById
{
    public class GetRoleByIdQueryRequest : IRequest<GetRoleByIdQueryResponse>
    {
        public Guid RoleId { get; set; }
    }
}
