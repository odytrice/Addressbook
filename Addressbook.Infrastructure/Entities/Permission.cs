using System.Collections;
using System.Collections.Generic;

namespace Addressbook.Infrastructure.Entities
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string Name { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }
}