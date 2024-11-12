// LexiAuthenAPI.Infrastructure/Repositories/PermissionRepository.cs
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission> GetByIdWithPermissionUipermissionAsync(int id)
        {
            return await _context.Permissions
                .Include(p => p.PermissionUipermissions) // Include UipermissionPermissions
                .ThenInclude(up => up.Uipermission) // Include Uipermission data
                .FirstOrDefaultAsync(p => p.Id == id);
        }



        public async Task<Permission> AddAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task UpdateAsync(Permission permission)
        {
            _context.Entry(permission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
