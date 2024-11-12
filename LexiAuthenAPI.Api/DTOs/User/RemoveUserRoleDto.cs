// LexiAuthenAPI.Api/DTOs/User/RemoveUserRoleDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.User
{
    public class RemoveUserRoleDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }
    }
}
