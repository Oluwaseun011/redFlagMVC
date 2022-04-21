using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Interfaces
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        Task<Report> Get(int id);
        Task<Report> Get(Expression<Func<Report, bool>> expression);
        Task<IEnumerable<Report>> GetSelected(List<int> ids);
        Task<IEnumerable<Report>> GetSelected(Expression<Func<Report, bool>> expression);
        Task<IEnumerable<Report>> GetAll();
    }
}
