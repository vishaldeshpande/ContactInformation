using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactInformation.DataService
{
    public interface IGenericRepository<T>
    {
        T Get(Int32 id);
        List<T> GetAll();
        IEnumerable<T> Filter(Expression<Func<T, bool>> expression);
        int Post(T entity);
        int Put(T entity, int id);
        int Remove(T entity);

        List<T> Get(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes);

        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
    }
}
