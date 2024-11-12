// LexiAuthenAPI.Api/DTOs/Role/RemoveUserFromRoleDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Role
{
    public class RemoveUserFromRoleDto
    {
        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
