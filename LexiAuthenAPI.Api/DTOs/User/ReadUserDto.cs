// LexiAuthenAPI.Api/DTOs/User/ReadUserDto.cs
using System.Collections.Generic;
using LexiAuthenAPI.Api.DTOs.Role;

namespace LexiAuthenAPI.Api.DTOs.User
{
    public class ReadUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Optionally include roles
        public List<ReadRoleDto>? Roles { get; set; }
    }
}
