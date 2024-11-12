// LexiAuthenAPI.Api/DTOs/UIPermission/CreateUIPermissionDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Uipermission
{
    public class CreateUipermissionDto
    {
        [Required(ErrorMessage = "Code is required.")]
        public required string Code { get; set; } 

        [Required(ErrorMessage = "ComponentName is required.")]
        public required string ComponentName { get; set; } 

        [Required(ErrorMessage = "AccessLevel is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "AccessLevel must be a valid integer value.")]
        public required int AccessLevel { get; set; } // Map to UIPermissionAccessLevel enum
    }
}
