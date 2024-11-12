// LexiAuthenAPI.Infrastructure/Repositories/RefreshTokenRepository.cs
using Microsoft.EntityFrameworkCore;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Data;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Refreshtoken> _dbSet;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Refreshtoken>();
        }

        public async Task<Refreshtoken> GetByTokenAsync(string token)
        {
            return await _dbSet
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddAsync(Refreshtoken refreshToken)
        {
            await _dbSet.AddAsync(refreshToken);
        }

        public void Update(Refreshtoken refreshToken)
        {
            _dbSet.Update(refreshToken);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
