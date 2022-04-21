using Microsoft.EntityFrameworkCore;
using redFlag.Context;
using redFlag.Entities;
using redFlag.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Implementations
{
    public class AgencyRepository : BaseRepository<Agency>, IAgencyRepository
    {
        
        public AgencyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Agency> Get(int id)
        {
            return await _context.Agencies
                .Include(a => a.Branches)
                .Include(b => b.Staffs).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
        }

        public async Task<IEnumerable<Agency>> GetSelected(List<int> ids)
        {
            return await _context.Agencies
                .Include(a => a.Branches)
                .Include(b => b.Staffs)
                .Where(c => ids.Contains(c.Id) && c.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Agency> Get(Expression<Func<Agency, bool>> expression)
        {
            return await _context.Agencies
                .Include(a => a.Branches)
                .Include(b => b.Staffs)
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Agency>> GetAll()
        {
            return await _context.Agencies
                .Include(a => a.Branches)
                .Include(b => b.Staffs).ThenInclude(c => c.User)
                .Where(c => c.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Agency>> GetSelected(Expression<Func<Agency, bool>> expression)
        {
            return await _context.Agencies
                .Include(a => a.Branches)
                .Include(b => b.Staffs)
                .Where(expression)
                .ToListAsync();
        }
    }
}
