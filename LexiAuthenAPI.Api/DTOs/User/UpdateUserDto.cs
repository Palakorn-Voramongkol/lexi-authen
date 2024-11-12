// LexiAuthenAPI.Api/DTOs/User/UpdateUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.User
{
    public class UpdateUserDto
    {
        
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; }
    }
}
