using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Interfaces
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Task<Staff> Get(int id);
        Task<Staff> Get(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetSelected(List<int> ids);
        Task<IEnumerable<Staff>> GetSelected(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetAll();
    }
}
