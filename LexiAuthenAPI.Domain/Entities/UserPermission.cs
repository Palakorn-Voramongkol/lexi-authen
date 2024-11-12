// LexiAuthenAPI.Domain/Entities/RolePermission.cs
namespace LexiAuthenAPI.Domain.Entities
{
    public class UserPermission
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
