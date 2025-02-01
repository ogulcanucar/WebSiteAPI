using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.DTOs.Auth;
using WebSiteAPI.Application.DTOs.User;

namespace WebSiteAPI.Application.Abstractions.Service
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task<List<UserDto>> GetAllUsersAsync(); // **Tüm kullanıcıları çeken
        Task<List<ListUser>> GetAllUserAsync(int page, int size);
        Task DeleteUserAsync(Guid UserId);
        Task<List<string>> GetRolesByUserIdAsync(string userId); // ✅ EKLENDİ
        // Güncellenmiş Giriş Metodu - SignInResult yerine Token döndürüyor
        Task<AuthResponse> AuthenticateAsync(string username, string password);

        // Refresh Token ile yeni token alma
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
        Task<List<string>> GetUserRolesAsync(string userId);

        int TotalUsersCount { get; }
    }

}
