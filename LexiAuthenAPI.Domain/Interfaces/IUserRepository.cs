// LexiAuthenAPI.Domain/Interfaces/IUserRepository.cs
using LexiAuthenAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User> FindByUsernameAsync(string username);
        Task SaveChangesAsync();
        Task<User> GetByIdWithRoleUserAsync(int id);
        Task<User> GetByIdWithPermissionUserAsync(int id);
    }
}
