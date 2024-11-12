// LexiAuthenAPI.Api/DTOs/Permission/CreatePermissionDto.cs
namespace LexiAuthenAPI.Api.DTOs.Permission
{
    public class CreatePermissionDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PermissionType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
