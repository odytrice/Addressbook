using System.Collections.Generic;

namespace Addressbook.Core.Models
{
    public class RoleModel : Model
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public ICollection<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();
    }
}