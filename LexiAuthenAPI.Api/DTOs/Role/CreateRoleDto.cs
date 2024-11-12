// LexiAuthenAPI.Api/DTOs/Role/CreateRoleDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Role
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Code is required.")]
        public required string Code { get; set; } 

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public required string Name { get; set; } 

        [StringLength(250, ErrorMessage = "Description must not exceed 250 characters.")]
        public required string Description { get; set; }
    }
}
