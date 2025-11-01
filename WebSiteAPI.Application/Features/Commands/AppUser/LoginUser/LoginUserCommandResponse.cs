namespace WebSiteAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Roles { get; set; } = new List<string>(); // Roller eklendi

        // 🔹 Cookie için eklenen alanlar:
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}