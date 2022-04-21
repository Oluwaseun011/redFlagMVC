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
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Staff> Get(int id)
        {
            return await _context.Staffs
                .Include(a => a.User)
                .Include(b => b.Agency)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Staff> Get(Expression<Func<Staff, bool>> expression)
        {
            return await _context.Staffs
                .Include(a => a.User)
                .Include(b => b.Agency)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await _context.Staffs
                .Include(a => a.User)
                .Include(b => b.Agency)
                .ToListAsync();
        }

        public async Task<IEnumerable<Staff>> GetSelected(List<int> ids)
        {
            return await _context.Staffs
                .Include(a => a.User)
                .Include(b => b.Agency)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Staff>> GetSelected(Expression<Func<Staff, bool>> expression)
        {
            return await _context.Staffs
                .Include(a => a.User)
                .Include(b => b.Agency)
                .Where(expression)
                .ToListAsync();
        }
    }
}
