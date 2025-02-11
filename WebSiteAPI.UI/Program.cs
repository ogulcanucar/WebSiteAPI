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
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Error/AccessDenied";
    });

builder.Services.AddAuthorization();


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
