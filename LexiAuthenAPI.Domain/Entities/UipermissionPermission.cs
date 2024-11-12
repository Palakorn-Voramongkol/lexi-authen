namespace LexiAuthenAPI.Domain.Entities
{
    public class UipermissionPermission
    {
        // Composite Key
        public int UipermissionId { get; set; }
        public Uipermission Uipermission { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

    }
}
