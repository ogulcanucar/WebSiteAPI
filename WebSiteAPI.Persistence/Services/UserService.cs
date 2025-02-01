using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.DTOs.Auth;
using WebSiteAPI.Application.DTOs.User;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public int TotalUsersCount => _userManager.Users.Count();

        // ✅ Kullanıcı oluşturma (GUID destekli)
        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new AppUser()
            {
                Id = Guid.NewGuid(), // ✅ GUID olarak ID oluşturduk
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.Username,
                Email = model.Email,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla kaydedildi.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        // ✅ Kullanıcı silme (GUID destekli)
        public async Task DeleteUserAsync(Guid userId) // String yerine Guid kullan
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId.ToString()); // Guid'i stringe çevir
            if (appUser != null)
            {
                await _userManager.DeleteAsync(appUser);
            }
        }

        // ✅ Kullanıcı listeleme (GUID uyumlu)
        public async Task<List<ListUser>> GetAllUserAsync(int page, int size)
        {
            var users = await _userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return users.Select(u => new ListUser
            {
                Id = u.Id.ToString(), // ✅ GUID olarak döndür
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                TwoFactorEnabled = u.TwoFactorEnabled
            }).ToList();
        }

        // ✅ Kullanıcı kimlik doğrulama (Aynı kaldı)
        public async Task<SignInResult> AuthenticateAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
            return result;
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return _userManager.Users
                .Select(user => new UserDto
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName
                })
                .ToList();
        }
        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        Task<AuthResponse> IUserService.AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetRolesByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
