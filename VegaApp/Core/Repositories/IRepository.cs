using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vega.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(long id);
        Task<IEnumerable<TEntity>> GetAll();
        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
    }
}
