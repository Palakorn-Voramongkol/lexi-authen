// LexiAuthenAPI.Domain/Interfaces/IUiitemRepository.cs
using LexiAuthenAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Domain.Interfaces
{
    public interface IUiitemRepository
    {
        Task<IEnumerable<Uiitem>> GetAllAsync();
        Task<Uiitem> GetByIdAsync(int id);
        Task<Uiitem> AddAsync(Uiitem uiitem);
        Task UpdateAsync(Uiitem uiitem);
        Task DeleteAsync(int id);
    }
}
