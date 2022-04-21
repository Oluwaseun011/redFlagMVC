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
    public class InitiatorRepository : BaseRepository<Initiator>, IInitiatorRepository
    {
        public InitiatorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Initiator> Get(int id)
        {
            return await _context.Initiators
                .Include(b => b.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Initiator> Get(Expression<Func<Initiator, bool>> expression)
        {
            return await _context.Initiators
                .Include(b => b.User)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Initiator>> GetAll()
        {
            return await _context.Initiators
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Initiator>> GetSelected(List<int> ids)
        {
            return await _context.Initiators
                .Include(b => b.User)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Initiator>> GetSelected(Expression<Func<Initiator, bool>> expression)
        {
            return await _context.Initiators
                .Include(b => b.User)
                .Where(expression)
                .ToListAsync();
        }
    }
}
