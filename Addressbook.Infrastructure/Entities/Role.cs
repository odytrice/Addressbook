using System.Collections.Generic;

namespace Addressbook.Infrastructure.Entities
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }
}