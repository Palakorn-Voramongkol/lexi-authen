// LexiAuthenAPI.Domain/Api/Services/IUiitemService.cs
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Api.DTOs.Uiitem;

namespace LexiAuthenAPI.Api.Services
{
    public interface IUiitemService
    {
        Task<ReadUiitemDto> CreateUiitemAsync(CreateUiitemDto CreateUiitemDto);
        Task<ReadUiitemDto> GetUiitemByIdAsync(int id);
        Task<IEnumerable<ReadUiitemDto>> GetAllUiitemsAsync();
        Task<ReadUiitemDto> UpdateUiitemAsync(int id, UpdateUiitemDto uiitemDto);
        Task<bool> DeleteUiitemAsync(int id);
    }
}
