// LexiAuthenAPI.Api/Services/IUserService.cs
using LexiAuthenAPI.Api.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDto>> GetAllUsersAsync();
        Task<ReadUserDto> GetUserByIdAsync(int id);
        Task<ReadUserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<ReadUserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> AddUserRoleAsync(AddUserRoleDto addUserRoleDto);
        Task<bool> RemoveUserRoleAsync(RemoveUserRoleDto removeUserRoleDto);
        Task<bool> AddPermissionUserAsync(AddPermissionUserDto addPermissionUserDto);
        Task<bool> RemovePermissionUserAsync(RemovePermissionUserDto removePermissionUserDto);
    }
}
