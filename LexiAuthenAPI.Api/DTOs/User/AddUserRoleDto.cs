// LexiAuthenAPI.Api/DTOs/User/AddUserRoleDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.User
{
    public class AddUserRoleDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public required int UserId { get; set; }

        [Required(ErrorMessage = "RoleId is required.")]
        public required int RoleId { get; set; }
    }
}
