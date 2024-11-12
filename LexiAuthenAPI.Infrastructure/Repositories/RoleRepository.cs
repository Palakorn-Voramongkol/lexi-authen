// LexiAuthenAPI.Infrastructure/Repositories/RoleRepository.cs
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> GetByIdWithUserRolesAsync(int roleId)
        {
            return await _context.Roles
               .Include(r => r.UserRoles) // Include UserRoles
               .ThenInclude(ur => ur.User) // Optionally include User data
               .FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<Role> GetByIdWithPermissionRolesAsync(int roleId)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions) // Include RolePermissions
                .ThenInclude(rp => rp.Permission) // Include Permission data
                .FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<Role> AddAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
