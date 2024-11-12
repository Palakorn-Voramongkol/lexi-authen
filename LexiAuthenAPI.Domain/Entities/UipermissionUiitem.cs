// LexiAuthenAPI.Domain/Entities/UipermissionUiitem.cs
namespace LexiAuthenAPI.Domain.Entities
{
    public class UipermissionUiitem
    {
        public int UipermissionId { get; set; }
        public Uipermission Uipermission { get; set; }

        public int UiitemId { get; set; }
        public Uiitem Uiitem { get; set; }
    }
}
