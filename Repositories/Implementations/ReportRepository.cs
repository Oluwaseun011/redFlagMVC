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
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Report> Get(int id)
        {
            return await _context.Reports
                .Include(b => b.Initiator)
                .Include(c => c.Branch)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Report> Get(Expression<Func<Report, bool>> expression)
        {
            return await _context.Reports
                .Include(b => b.Initiator).ThenInclude(c => c.User)
                .Include(c => c.Branch)
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            return await _context.Reports
                .Include(b => b.Initiator).ThenInclude(c => c.User)
                .Include(c => c.Branch)
                .Where(a => a.IsDeleted == false && a.ReportStatus != Enums.ReportStatus.Cancelled)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetSelected(List<int> ids)
        {
            return await _context.Reports
                .Include(b => b.Initiator).ThenInclude(c => c.User)
                .Include(c => c.Branch)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false && a.ReportStatus != Enums.ReportStatus.Cancelled)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetSelected(Expression<Func<Report, bool>> expression)
        {
            return await _context.Reports
                .Include(b => b.Initiator).ThenInclude(c => c.User)
                .Include(c => c.Branch)
                .Where(expression)
                .ToListAsync();
        }
    }
}
