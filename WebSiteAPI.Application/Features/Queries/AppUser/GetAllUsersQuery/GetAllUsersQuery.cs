using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.DTOs.User;
namespace WebSiteAPI.Application.Features.Queries.AppUser.GetAllUsersQuery
{
    public class GetAllUsersQuery : IRequest<List<UserDto>> { }
}
