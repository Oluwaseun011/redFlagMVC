using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Interfaces
{
    public interface IBranchRepository : IBaseRepository<Branch>
    {
        Task<Branch> Get(int id);
        Task<Branch> Get(Expression<Func<Branch, bool>> expression);
        Task<IEnumerable<Branch>> GetSelected(List<int> ids);
        Task<IEnumerable<Branch>> GetSelected(Expression<Func<Branch, bool>> expression);
        Task<IEnumerable<Branch>> GetAll();
    }
}
