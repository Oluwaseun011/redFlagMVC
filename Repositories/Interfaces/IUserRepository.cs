using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace redFlag.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> Get(int id);
        Task<User> Get(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetSelected(List<int> ids);
        Task<IEnumerable<User>> GetSelected(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAll();
    }
}
