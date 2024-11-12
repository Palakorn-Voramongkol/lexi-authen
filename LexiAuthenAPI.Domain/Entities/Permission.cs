// LexiAuthenAPI.Domain/Entities/Permission.cs
using System.Collections.Generic;

namespace LexiAuthenAPI.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PermissionType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation property for many-to-many relationship
        public ICollection<UserPermission> UserPermissions { get; } = [];
        public ICollection<RolePermission> RolePermissions { get; } = [];
        public ICollection<PermissionUipermission> PermissionUipermissions { get; } = [];
    }
}
