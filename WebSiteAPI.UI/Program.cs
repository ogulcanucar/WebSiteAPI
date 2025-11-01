using WebSiteAPI.Application;
using WebSiteAPI.Persistence;
using WebSiteAPI.Infrastructure;
using WebSiteAPI.Infrastructure.Services.Auth;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Abstractions.Storage;
using WebSiteAPI.Infrastructure.Services.Storage;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Infrastructure.Services.Storage.Local;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// **IHttpContextAccessor Servisini Ekleyelim**
builder.Services.AddHttpContextAccessor();

// **Bağımlılıkları Kaydet**
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStorage, LocalStorage>();
builder.Services.AddScoped<IStorageService, StorageService>();

// **Diğer Servisleri Yükle**
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices(); // Burada MediatR zaten kayıt ediliyor.
builder.Services.AddInfrastructureServices();

builder.Services.AddControllersWithViews();

// **Cookie Authentication Kullan**
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
    options.AccessDeniedPath = "/Error/AccessDenied";
    options.Cookie.Name = "MyAppAuth";
    options.Cookie.SameSite = SameSiteMode.Lax; // Local test
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});


builder.Services.AddAuthorization(options =>
{
    // Rol yönetimi (RoleManage) — SuperAdmin her zaman geçsin, yoksa RoleManager
    options.AddPolicy("RoleManage", policy =>
    {
        policy.RequireAssertion(context =>
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                string.Equals(c.Value, "SuperAdmin", StringComparison.OrdinalIgnoreCase)))
                return true;

            return context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                string.Equals(c.Value, "RoleManager", StringComparison.OrdinalIgnoreCase));
        });
    });

    // Kullanıcı yönetimi (UserManage) — SuperAdmin ya da UserManager
    options.AddPolicy("UserManage", policy =>
    {
        policy.RequireAssertion(context =>
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                string.Equals(c.Value, "SuperAdmin", StringComparison.OrdinalIgnoreCase)))
                return true;

            return context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                string.Equals(c.Value, "UserManager", StringComparison.OrdinalIgnoreCase));
        });
    });

    // Ürün yönetimi (ProductManage) — SuperAdmin ya da ProductManager
    options.AddPolicy("ProductManage", policy =>
    {
        policy.RequireAssertion(context =>
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                string.Equals(c.Value, "SuperAdmin", StringComparison.OrdinalIgnoreCase)))
                return true;

            return context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                string.Equals(c.Value, "ProductManager", StringComparison.OrdinalIgnoreCase));
        });
    });

    // İleride ekleyeceğin policy'leri aynı pattern ile ekle (SuperAdmin override + ilgili rol)
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Önce Authentication
app.UseAuthorization();  // Sonra Authorization


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStatusCodePagesWithRedirects("/Error/AccessDenied");

app.Run();
