// LexiAuthenAPI.Api/DTOs/Uipermission/RemoveUiitemUipermissionDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Uipermission
{
    public class RemoveUiitemUipermissionDto
    {
        [Required(ErrorMessage = "UiitemId is required.")]
        public int UiitemId { get; set; }

        [Required(ErrorMessage = "UipermissionId is required.")]
        public int UipermissionId { get; set; }
    }
}
