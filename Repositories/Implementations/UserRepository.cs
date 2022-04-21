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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            var user = await _context.Users
                .Include(a => a.Staff)
                .ThenInclude(d => d.Agency)
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(expression);
            return user;
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSelected(List<int> ids)
        {
            return await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSelected(Expression<Func<User, bool>> expression)
        {
            return await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .Where(expression)
                .ToListAsync();
        }
    }
}
