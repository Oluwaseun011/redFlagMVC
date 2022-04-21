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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Role> Get(int id)
        {
            return await _context.Roles
                .Where(a => a.IsDeleted == false)
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Role> Get(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
                .Where(a => a.IsDeleted == false)
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles
                .Where(a => a.IsDeleted == false)
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelected(List<int> ids)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.User)
                .Where(a => ids.Contains(a.Id) && a.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelected(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.User)
                .Where(expression)
                .ToListAsync();
        }
    }
}
