using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dqt.datalayer.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task SetUpDB();
        Task<IEnumerable<T>> FindAllAsync();

        Task<int> InsertAsync(T entity);
    }
}
