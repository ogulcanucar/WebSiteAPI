using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.DTOs.User;
using a = WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Application.Features.Queries.AppUser.GetAllUsersQuery
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly UserManager<a.AppUser> _userManager;

        public GetAllUsersQueryHandler(UserManager<a.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users
                .Select(user => new UserDto
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName
                })
                .ToList();

            return users;
        }
    }
}
