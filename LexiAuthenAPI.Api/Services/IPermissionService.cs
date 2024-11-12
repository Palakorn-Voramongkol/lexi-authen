// LexiAuthenAPI.Domain/Interfaces/IPermissionService.cs
using LexiAuthenAPI.Api.DTOs.Permission;

namespace LexiAuthenAPI.Api.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<ReadPermissionDto>> GetAllPermissionsAsync();
        Task<ReadPermissionDto> GetPermissionByIdAsync(int id);
        Task<ReadPermissionDto> CreatePermissionAsync(CreatePermissionDto createPermissionDto);
        Task<ReadPermissionDto> UpdatePermissionAsync(int id, UpdatePermissionDto updatePermissionDto);
        Task<bool> DeletePermissionAsync(int id);
        Task<bool> AddUipermissionPermissionAsync(AddUipermissionPermissionDto addUipermissionPermissionDto);
        Task<bool> RemoveUipermissionPermissionAsync(RemoveUipermissionPermissionDto removeUipermissionPermissionDto);
    }
}
