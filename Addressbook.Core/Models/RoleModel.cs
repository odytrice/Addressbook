using System.Collections.Generic;

namespace Addressbook.Core.Models
{
    public class RoleModel
    {
        public int RoleID { get; set; }
        public string  Name { get; set; }
        public ICollection<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();
    }
}