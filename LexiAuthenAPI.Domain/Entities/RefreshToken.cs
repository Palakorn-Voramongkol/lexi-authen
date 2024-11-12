// src/LexiAuthenAPI.Domain/Entities/Refreshtoken.cs
using System;

namespace LexiAuthenAPI.Domain.Entities
{
    public class Refreshtoken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
