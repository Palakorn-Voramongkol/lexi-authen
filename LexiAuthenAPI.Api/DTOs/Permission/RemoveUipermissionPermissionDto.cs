// LexiAuthenAPI.Api/DTOs/Permission/RemoveUipermissionPermissionDto.cs
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Permission
{
    public class RemoveUipermissionPermissionDto
    {
        [Required(ErrorMessage = "PermissionId is required.")]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "UipermissionId is required.")]
        public int UipermissionId { get; set; }
    }
}