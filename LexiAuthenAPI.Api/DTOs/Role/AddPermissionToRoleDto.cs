// LexiAuthenAPI.Api/DTOs/Role/AddPermissionToRoleDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Role
{
    public class AddPermissionToRoleDto
    {
        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "PermissionId is required.")]
        public int PermissionId { get; set; }
    }
}
