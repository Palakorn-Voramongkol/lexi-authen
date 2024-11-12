// LexiAuthenAPI.Domain/Entities/Role.cs
using System.Collections.Generic;

namespace LexiAuthenAPI.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation properties
        // Initialize the UserRoles collection
        public ICollection<UserRole> UserRoles { get; } = [];
        public ICollection<RolePermission> RolePermissions { get; } = [];
    }
}
