﻿// LexiAuthenAPI.Api/DTOs/Uiitem/ReadUiitemDto.cs
namespace LexiAuthenAPI.Api.DTOs.Uiitem
{
    public class ReadUiitemDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
