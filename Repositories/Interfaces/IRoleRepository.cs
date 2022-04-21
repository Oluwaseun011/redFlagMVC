using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> Get(int id);
        Task<Role> Get(Expression<Func<Role, bool>> expression);
        Task<IEnumerable<Role>> GetSelected(List<int> ids);
        Task<IEnumerable<Role>> GetSelected(Expression<Func<Role, bool>> expression);
        Task<IEnumerable<Role>> GetAll();
    }
}
