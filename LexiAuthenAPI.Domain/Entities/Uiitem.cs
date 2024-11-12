// LexiAuthenAPI.Domain/Entities/Uiitem.cs
using System.Collections.Generic;

namespace LexiAuthenAPI.Domain.Entities
{
    public class Uiitem
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<UipermissionUiitem> UipermissionUiitem { get; } = [];
    }
}
