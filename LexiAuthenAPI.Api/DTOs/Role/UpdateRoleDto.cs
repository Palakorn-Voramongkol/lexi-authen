// LexiAuthenAPI.Api/DTOs/Role/UpdateRoleDto.cs
namespace LexiAuthenAPI.Api.DTOs.Role
{
    public class UpdateRoleDto
    {
        public string? Code { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
