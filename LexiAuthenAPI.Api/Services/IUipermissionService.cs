// LexiAuthenAPI.Domain/Interfaces/IUipermissionService.cs
using LexiAuthenAPI.Api.DTOs.Uipermission;

namespace LexiAuthenAPI.Api.Services
{
    public interface IUipermissionService
    {
        Task<IEnumerable<ReadUipermissionDto>> GetAllUipermissionsAsync();
        Task<ReadUipermissionDto> GetUipermissionByIdAsync(int id);
        Task<ReadUipermissionDto> CreateUipermissionAsync(CreateUipermissionDto createUipermissionDto);
        Task<ReadUipermissionDto> UpdateUipermissionAsync(int id, UpdateUipermissionDto updateUipermissionDto);
        Task<bool> DeleteUipermissionAsync(int id);
        Task<bool> AddUiitemUipermissionAsync(AddUiitemUipermissionDto addUiitemUipermissionDto);
        Task<bool> RemoveUiitemUipermissionAsync(RemoveUiitemUipermissionDto removeUiitemUipermissionDto);
    }
}
