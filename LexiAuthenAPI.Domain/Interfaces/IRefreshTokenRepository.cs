// src/LexiAuthenAPI.Domain/Interfaces/IRefreshTokenRepository.cs
using LexiAuthenAPI.Domain.Entities;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(Refreshtoken refreshToken);
        Task<Refreshtoken> GetByTokenAsync(string token);
        void Update(Refreshtoken refreshToken);
        Task SaveChangesAsync();
    }
}
