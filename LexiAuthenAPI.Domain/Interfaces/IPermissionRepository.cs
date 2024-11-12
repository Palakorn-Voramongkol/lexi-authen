// LexiAuthenAPI.Domain/Interfaces/IPermissionRepository.cs
using LexiAuthenAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission> GetByIdAsync(int id);
        Task<Permission> GetByIdWithPermissionUipermissionAsync(int id);
        Task<Permission> AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(int id);
    }
}
