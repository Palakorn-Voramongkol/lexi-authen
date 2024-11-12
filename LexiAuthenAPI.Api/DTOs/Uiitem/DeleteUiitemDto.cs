// LexiAuthenAPI.Api/DTOs/Uiitem/DeleteUiitemDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Uiitem
{
    public class DeleteUiitemDto
    {
        [Required(ErrorMessage = "Id is required for deletion.")]
        public int Id { get; set; }
    }
}
