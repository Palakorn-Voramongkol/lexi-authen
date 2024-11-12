// LexiAuthenAPI.Api/Services/IRoleService.cs
using LexiAuthenAPI.Api.DTOs.Role;

namespace LexiAuthenAPI.Api.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<ReadRoleDto>> GetAllRolesAsync();
        Task<ReadRoleDto> GetRoleByIdAsync(int id);
        Task<ReadRoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<ReadRoleDto> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto); // Updated signature
        Task<bool> DeleteRoleAsync(int id); // Simplified to accept only the ID
        Task<bool> AddUserToRoleAsync(AddUserToRoleDto addUserToRoleDto);
        Task<bool> RemoveUserFromRoleAsync(RemoveUserFromRoleDto removeUserFromRoleDto);
        Task<bool> AddPermissionToRoleAsync(AddPermissionToRoleDto addPermissionToRoleDto);
        Task<bool> RemovePermissionFromRoleAsync(RemovePermissionFromRoleDto removePermissionFromRoleDto);
    }
}
