// LexiAuthenAPI.Domain/Interfaces/IUipermissionRepository.cs
using LexiAuthenAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Domain.Interfaces
{
    public interface IUipermissionRepository
    {
        Task<IEnumerable<Uipermission>> GetAllAsync();
        Task<Uipermission> GetByIdAsync(int id);
        Task<Uipermission> GetByIdWithUiitemsAsync(int id);
        Task<Uipermission> AddAsync(Uipermission uipermission);
        Task UpdateAsync(Uipermission uipermission);
        Task DeleteAsync(int id);
    }
}
