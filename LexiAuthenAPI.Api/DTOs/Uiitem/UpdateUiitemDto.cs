// LexiAuthenAPI.Api/DTOs/Uiitem/UpdateUiitemDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Uiitem
{
    public class UpdateUiitemDto
    {

        [StringLength(50, ErrorMessage = "Code must not exceed 50 characters.")]
        public string? Code { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string? Name { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Description must not exceed 250 characters.")]
        public string? Description { get; set; } = string.Empty;
    }
}
