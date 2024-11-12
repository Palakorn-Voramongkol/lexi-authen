// src/LexiAuthenAPI.Domain/Entities/User.cs
using System.Collections.Generic;

namespace LexiAuthenAPI.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Password hash

        // Navigation property for many-to-many relationship
        public ICollection<UserRole> UserRoles { get; } = [];

        // Navigation property for refresh tokens
        public ICollection<Refreshtoken> Refreshtokens { get;} = [];

        // Navigation property for many-to-many relationship
        public ICollection<UserPermission> UserPermissions { get; } = [];
    }
}
