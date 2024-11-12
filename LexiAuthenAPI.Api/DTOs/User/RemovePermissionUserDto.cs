// LexiAuthenAPI.Api/DTOs/User/RemovePermissionFromUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.User
{
    public class RemovePermissionUserDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "PermissionId is required.")]
        public int PermissionId { get; set; }
    }
}
