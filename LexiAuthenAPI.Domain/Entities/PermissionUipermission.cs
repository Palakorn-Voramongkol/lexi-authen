// LexiAuthenAPI.Domain/Entities/UipermissionUiitem.cs
namespace LexiAuthenAPI.Domain.Entities
{
    public class PermissionUipermission
    {
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        public int UipermissionId { get; set; }
        public Uipermission Uipermission { get; set; }
    }

}
