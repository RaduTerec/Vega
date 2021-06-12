using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core;

namespace Vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext _vegaDbContext;
        public UnitOfWork(VegaDbContext vegaDbContext)
        {
            _vegaDbContext = vegaDbContext;

        }
        public async Task<int> Complete()
        {
            try
            {
                return await _vegaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return 0;
            }
        }
    }
}