// LexiAuthenAPI.Domain/Interfaces/IRoleRepository.cs
using LexiAuthenAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        Task<Role> GetByIdWithUserRolesAsync(int id);
        Task<Role> GetByIdWithPermissionRolesAsync(int id);
        Task<Role> AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);
        
    }
}
