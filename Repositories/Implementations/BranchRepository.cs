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
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Branch> Get(int id)
        {
            return await _context.Branches
                .Include(b => b.Agency)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<Branch> Get(Expression<Func<Branch, bool>> expression)
        {
            return await _context.Branches
                .Include(b => b.Agency)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            return await _context.Branches
                .Include(b => b.Agency)
                .Where(a => a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetSelected(List<int> ids)
        {
            return await _context.Branches
                .Include(b => b.Agency)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetSelected(Expression<Func<Branch, bool>> expression)
        {
            return await _context.Branches
                .Include(b => b.Agency)
                .Where(expression)
                .ToListAsync();
        }
    }
}
