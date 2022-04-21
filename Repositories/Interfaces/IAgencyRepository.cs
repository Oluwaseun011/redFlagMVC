using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Interfaces
{
    public interface IAgencyRepository : IBaseRepository<Agency>
    {
        Task<Agency> Get(int id);
        Task<Agency> Get(Expression<Func<Agency, bool>> expression);
        Task<IEnumerable<Agency>> GetSelected(List<int> ids);
        Task<IEnumerable<Agency>> GetSelected(Expression<Func<Agency, bool>> expression);
        Task<IEnumerable<Agency>> GetAll();
    }
}
