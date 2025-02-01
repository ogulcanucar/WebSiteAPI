using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities;
using WebSiteAPI.Domain.Entities.Common;
using WebSiteAPI.Domain.Entities.Identity;

namespace WebSiteAPI.Persistence.Contexts
{
    public class WebSiteAPIContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public WebSiteAPIContext(DbContextOptions<WebSiteAPIContext> options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<Endpoint> Endpoint { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Mail> Mail { get; set; }
        public DbSet<AppRole> Roles { get; set; } // Roller tablosu
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<AppRolePermission> RolePermissions { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            Guid superAdminId= Guid.NewGuid();
            Guid superAdminRoleId= Guid.NewGuid();
            var superAdmin = new AppUser
            {
                Id = superAdminId,
                UserName = "ogulcanucar",
                NormalizedUserName = "OGULCANUCAR",
                Email = "ogulcan.ucar@hotmail.com",
                NormalizedEmail = "OGULCAN.UCAR@HOTMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = "Oğulcan",
                Surname = "Uçar"
            };
            PasswordHasher<AppUser>hasher = new PasswordHasher<AppUser>();
            superAdmin.PasswordHash = hasher.HashPassword(superAdmin, "123456");
            var superAdminRole = new AppRole
            {
                Id = superAdminRoleId,
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"
            };
            var permissions = new[]
           {
                new Permission { Id = Guid.NewGuid(), Name = "Ekleme", CreatedBy = superAdminId.ToString(), UpdatedBy = superAdminId.ToString(), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow },
                new Permission { Id = Guid.NewGuid(), Name = "Silme", CreatedBy = superAdminId.ToString(), UpdatedBy = superAdminId.ToString(), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow },
                new Permission { Id = Guid.NewGuid(), Name = "Güncelleme", CreatedBy = superAdminId.ToString(), UpdatedBy = superAdminId.ToString(), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow },
                new Permission { Id = Guid.NewGuid(), Name = "Listeleme", CreatedBy = superAdminId.ToString(), UpdatedBy = superAdminId.ToString(), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow }
            };

            // 📌 Verileri Seed Olarak Ekle
            builder.Entity<AppUser>().HasData(superAdmin);
            builder.Entity<AppRole>().HasData(superAdminRole);
            builder.Entity<Permission>().HasData(permissions);
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> { UserId = superAdminId, RoleId = superAdminRoleId });
            builder.Entity<Order>()
                .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Entity<Cart>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Cart)
                .HasForeignKey<Order>(b => b.Id)
                 .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Order>()
            //    .HasOne(o => o.CompletedOrder)
            //    .WithOne(c => c.Order)
            //    .HasForeignKey<CompletedOrder>(c => c.OrderId);

            // Roller ile Permission (Yetki) arasındaki Many-to-Many ilişkiyi AppRolePermission üzerinden kur
            builder.Entity<AppRolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<AppRolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.

            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
