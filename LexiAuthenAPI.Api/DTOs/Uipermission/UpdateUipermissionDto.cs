// LexiAuthenAPI.Api/DTOs/Uipermission/UpdateUipermissionDto.cs
using LexiAuthenAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LexiAuthenAPI.Api.DTOs.Uipermission
{
    public class UpdateUipermissionDto
    {

        public string? Code { get; set; } = string.Empty;

        public UIPermissionAccessLevel? AccessLevel { get; set; }

        public int? PermissionId { get; set; } // Assuming you might want to change the linked permission
    }
}
