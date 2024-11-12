// LexiAuthenAPI.Api/DTOs/Permission/ReadPermissionDto.cs
namespace LexiAuthenAPI.Api.DTOs.Permission
{
    public class ReadPermissionDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PermissionType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
