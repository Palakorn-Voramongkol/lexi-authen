// LexiAuthenAPI.Infrastructure/Repositories/UiitemRepository.cs
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Infrastructure.Repositories
{
    public class UiitemRepository : IUiitemRepository
    {
        private readonly ApplicationDbContext _context;

        public UiitemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Uiitem>> GetAllAsync()
        {
            return await _context.Uiitems
                .Include(ui => ui.UipermissionUiitem)
                    .ThenInclude(uip => uip.Uipermission)
                .ToListAsync();
        }

        public async Task<Uiitem> GetByIdAsync(int id)
        {
            return await _context.Uiitems
                .Include(ui => ui.UipermissionUiitem)
                    .ThenInclude(uip => uip.Uipermission)
                .FirstOrDefaultAsync(ui => ui.Id == id);
        }

        public async Task<Uiitem> AddAsync(Uiitem uiitem)
        {
            _context.Uiitems.Add(uiitem);
            await SaveChangesAsync();
            return uiitem;
        }

        public async Task UpdateAsync(Uiitem uiitem)
        {
            _context.Entry(uiitem).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var uiitem = await _context.Uiitems.FindAsync(id);
            if (uiitem != null)
            {
                _context.Uiitems.Remove(uiitem);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
