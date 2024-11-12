// LexiAuthenAPI.Api/DTOs/Role/ReadRoleDto.cs
namespace LexiAuthenAPI.Api.DTOs.Role
{
    public class ReadRoleDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
