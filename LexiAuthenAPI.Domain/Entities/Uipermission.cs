// LexiAuthenAPI.Domain/Entities/Uipermission.cs
using LexiAuthenAPI.Domain.Enums;
using System.Collections.Generic;

namespace LexiAuthenAPI.Domain.Entities
{
    public class Uipermission
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public UIPermissionAccessLevel AccessLevel { get; set; } // Updated to use the enum

        // Navigation properties
        public ICollection<PermissionUipermission> PermissionUipermissions { get; } = [];
        public ICollection<UipermissionUiitem> UipermissionUiitems { get; } = [];
    }
}
