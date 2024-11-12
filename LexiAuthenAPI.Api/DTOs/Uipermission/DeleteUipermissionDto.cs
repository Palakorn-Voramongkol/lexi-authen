// LexiAuthenAPI.Api/DTOs/Uipermission/DeleteUipermissionDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Uipermission
{
    public class DeleteUipermissionDto
    {
        [Required(ErrorMessage = "Id is required for deletion.")]
        public int Id { get; set; }
    }
}
