// LexiAuthenAPI.Api/DTOs/Uiitem/CreateUiitemDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Uiitem
{
    public class CreateUiitemDto
    {
        [Required(ErrorMessage = "Code is required.")]
        public required string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }
    }
}
