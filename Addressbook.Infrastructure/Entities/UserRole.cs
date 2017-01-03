namespace Addressbook.Infrastructure.Entities
{
    public class UserRole
    {
        public int UserRoleID { get; set; }
        public int UserId { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}