using System;
using System.Collections.Generic;

namespace WebSiteAPI.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }          // JWT Token
        public string RefreshToken { get; set; }   // Refresh Token
        public DateTime Expiration { get; set; }   // Token Geçerlilik Süresi
        public Guid UserId { get; set; }           // Kullanıcı Id (GUID)
        public string UserName { get; set; }       // Kullanıcı Adı
        public List<string> Roles { get; set; }    // Kullanıcının Rolleri
    }
}
