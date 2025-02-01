using WebSiteAPI.Domain.Entities.Common;

namespace WebSiteAPI.Domain.Entities.Identity
{
    public class AppRolePermission : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RoleId { get; set; }
        public AppRole Role { get; set; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
