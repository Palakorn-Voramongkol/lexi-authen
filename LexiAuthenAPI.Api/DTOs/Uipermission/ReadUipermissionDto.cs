// LexiAuthenAPI.Api/DTOs/Uipermission/ReadUipermissionDto.cs
using LexiAuthenAPI.Domain.Enums;

namespace LexiAuthenAPI.Api.DTOs.Uipermission
{
    public class ReadUipermissionDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public UIPermissionAccessLevel AccessLevel { get; set; }
        public string PermissionName { get; set; } = string.Empty; // Optional: Display the linked permission name
    }
}
