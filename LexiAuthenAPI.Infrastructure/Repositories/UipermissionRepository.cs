// LexiAuthenAPI.Infrastructure/Repositories/UipermissionRepository.cs
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Infrastructure.Repositories
{
    public class UipermissionRepository : IUipermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public UipermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Uipermission>> GetAllAsync()
        {
            return await _context.Uipermissions
                .Include(up => up.UipermissionUiitems)
                    .ThenInclude(uui => uui.Uiitem)
                .ToListAsync();
        }

        public async Task<Uipermission> GetByIdAsync(int id)
        {
            return await _context.Uipermissions
                .Include(up => up.UipermissionUiitems)
                    .ThenInclude(uui => uui.Uiitem)
                .FirstOrDefaultAsync(up => up.Id == id);
        }

        public async Task<Uipermission> GetByIdWithUiitemsAsync(int id)
        {
            return await _context.Uipermissions
                .Include(up => up.UipermissionUiitems) // Include the linking table
                .ThenInclude(ui => ui.Uiitem) // Include Uiitems data
                .FirstOrDefaultAsync(up => up.Id == id);
        }


        public async Task<Uipermission> AddAsync(Uipermission uipermission)
        {
            _context.Uipermissions.Add(uipermission);
            await _context.SaveChangesAsync();
            return uipermission;
        }

        public async Task UpdateAsync(Uipermission uipermission)
        {
            _context.Entry(uipermission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var uipermission = await _context.Uipermissions.FindAsync(id);
            if (uipermission != null)
            {
                _context.Uipermissions.Remove(uipermission);
                await _context.SaveChangesAsync();
            }
        }


    }
}
