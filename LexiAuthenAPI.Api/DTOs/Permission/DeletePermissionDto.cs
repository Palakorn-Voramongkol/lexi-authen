// LexiAuthenAPI.Api/DTOs/Permission/DeletePermissionDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Permission
{
    public class DeletePermissionDto
    {
        [Required(ErrorMessage = "Id is required for deletion.")]
        public int Id { get; set; }
    }
}
