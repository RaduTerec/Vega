using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Core.Repositories;

namespace Vega.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext dbContext)
        {
            _entities = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> Get(long id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }
    }
}
