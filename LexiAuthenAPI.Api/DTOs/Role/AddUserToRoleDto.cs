// LexiAuthenAPI.Api/DTOs/Role/AddUserToRoleDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Role
{
    public class AddUserToRoleDto
    {
        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
