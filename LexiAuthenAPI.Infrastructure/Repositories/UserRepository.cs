// LexiAuthenAPI.Infrastructure/Repositories/UserRepository.cs
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByIdWithRoleUserAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByIdWithPermissionUserAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserPermissions) // Include UserPermissions
                .ThenInclude(up => up.Permission) // Include Permission data
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await SaveChangesAsync();
            }
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
