using Microsoft.EntityFrameworkCore;
using WebSiteAPI.Domain.Entities.Identity;
using WebSiteAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Persistence.Repositories;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Persistence.Services;
using WebSiteAPI.Application.Repositories.Product;
using WebSiteAPI.Persistence.Repositories.Product;
using WebSiteAPI.Application.Repositories.ProductImage;
using WebSiteAPI.Persistence.Repositories.ProductImageFile;
using WebSiteAPI.Persistence.Repositories.ProductImage;
using WebSiteAPI.Persistence.Repositories.Invoice;
using WebSiteAPI.Application.Repositories.Slider;
using WebSiteAPI.Persistence.Repositories.Mail;
using WebSiteAPI.Application.Repositories.Permission;
using WebSiteAPI.Application.Repositories.Role;
using WebSiteAPI.Persistence.Repositories.Permission;
using WebSiteAPI.Persistence.Repositories.Role;
using WebSiteAPI.Application.Abstractions.Service.Authorization;
using WebSiteAPI.Persistence.Services.Authorization;

namespace WebSiteAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // ✅ DbContext Identity yapılandırması
            services.AddDbContext<WebSiteAPIContext>(options =>
                options.UseSqlServer(Configuration.ConnectionString));

            // ✅ Identity AppUser & AppRole (GUID Tabanlı) 
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<WebSiteAPIContext>()
            .AddDefaultTokenProviders(); // Token işlemleri için eklendi

            // ✅ Authentication ve Authorization Middleware’i aktif edelim
            services.AddAuthentication();
            services.AddAuthorization();

            // ✅ Role & Permission Servisleri
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IRoleService, RoleService>(); // 3. ADIM: RoleService EKLENDİ

            // ✅ Kullanıcı ve Genel Servisler
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IPermissionService, PermissionService>();


            // ✅ Repository Bağlamaları
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IInvoiceReadRepository, InvoiceReadRepository>();
            services.AddScoped<IInvoiceWriteRepository, InvoiceWriteRepository>();
            services.AddScoped<ISliderWriteRepository, SliderWriteRepository>();
            services.AddScoped<ISliderReadRepository, SliderReadRepository>();
            services.AddScoped<IMailReadRepository, MailReadRepository>();
            services.AddScoped<IMailWriteRepository, MailWriteRepository>();
            services.AddScoped<IRolePermissionReadRepository, RolePermissionReadRepository>();
            services.AddScoped<IRolePermissionWriteRepository, RolePermissionWriteRepository>();
            services.AddScoped<IPermissionReadRepository, PermissionReadRepository>();
            services.AddScoped<IPermissionWriteRepository, PermissionWriteRepository>();
        }
    }
}

