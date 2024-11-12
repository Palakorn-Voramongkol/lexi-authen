// LexiAuthenAPI.Domain/Entities/UserRole.cs

namespace LexiAuthenAPI.Domain.Entities
{
    public class UserRole
    {
        // Composite Key
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
