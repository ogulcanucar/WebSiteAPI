using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WebSiteAPI.Application;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Application.Abstractions.Storage;
using WebSiteAPI.Infrastructure;
using WebSiteAPI.Infrastructure.Services.Auth;
using WebSiteAPI.Infrastructure.Services.Storage;
using WebSiteAPI.Infrastructure.Services.Storage.Local;
using WebSiteAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

// **📌 IHttpContextAccessor Servisini Ekleyelim**
builder.Services.AddHttpContextAccessor();

// **📌 Bağımlılıkları Kaydet**
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStorage, LocalStorage>();
builder.Services.AddScoped<IStorageService, StorageService>();

// **📌 Diğer Servisleri Yükle**
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices(); // 📌 MediatR burada ekleniyor.
builder.Services.AddInfrastructureServices();

builder.Services.AddControllersWithViews();

// **📌 Authentication ve Authorization Servislerini Yapılandır**
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";        // Giriş sayfası yönlendirme
        options.LogoutPath = "/Auth/Logout";      // Çıkış sayfası yönlendirme
        options.AccessDeniedPath = "/Auth/Login"; // Yetkisiz giriş yönlendirme
        options.Cookie.HttpOnly = true;           // Güvenlik için çerezler sadece HTTP üzerinden erişilebilir olmalı
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // 7 gün boyunca oturum açık kalmalı
        options.SlidingExpiration = true;         // Kullanıcı aktifse süreyi yenile

        // **📌 Claims bilgisi doğrulama**
        options.Events.OnValidatePrincipal = async context =>
        {
            var userPrincipal = context.Principal;
            if (userPrincipal == null || userPrincipal.Claims.Count() == 0)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        };
    });

// **📌 Authorization Politikalarını Tanımla**
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Permission", "AdminAccess"));
    options.AddPolicy("ProductManage", policy => policy.RequireClaim("Permission", "Product.Add", "Product.Edit", "Product.Delete"));
});

// **📌 Uygulamayı Başlat**
var app = builder.Build();

// **📌 Hata Yönetimi**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// **📌 Middleware Kullanımı**
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();  // 📌 Kullanıcı kimlik doğrulaması burada aktif olmalı!
app.UseAuthorization();   // 📌 Yetkilendirme burada olmalı!

// **📌 Kullanıcı Claims Verisini Kontrol Et ve Yeniden Giriş Zorla**
app.Use(async (context, next) =>
{
    if (context.User.Identity.IsAuthenticated && context.User.Claims.Count() == 0)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        context.Response.Redirect("/Auth/Login");
        return;
    }
    await next();
});

// **📌 Varsayılan Rota Tanımlama**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
