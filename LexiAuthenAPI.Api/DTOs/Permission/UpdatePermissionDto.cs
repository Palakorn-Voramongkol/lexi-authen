// LexiAuthenAPI.Api/DTOs/Permission/UpdatePermissionDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Permission
{
    public class UpdatePermissionDto
    {

        public string? Code { get; set; } = string.Empty;

        public string? Name { get; set; } = string.Empty;

        public string? PermissionType { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;
    }
}
