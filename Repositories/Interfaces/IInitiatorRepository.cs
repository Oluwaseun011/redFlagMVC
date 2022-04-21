using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Interfaces
{
    public interface IInitiatorRepository : IBaseRepository<Initiator>
    {
        Task<Initiator> Get(int id);
        Task<Initiator> Get(Expression<Func<Initiator, bool>> expression);
        Task<IEnumerable<Initiator>> GetSelected(List<int> ids);
        Task<IEnumerable<Initiator>> GetSelected(Expression<Func<Initiator, bool>> expression);
        Task<IEnumerable<Initiator>> GetAll();
    }
}
