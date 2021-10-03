using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;
using Vega.Core.Repositories;

namespace Vega.Persistence
{
    public class MakeRepository : Repository<Make>, IMakeRepository
    {
        private readonly VegaDbContext _vegaDbContext;

        public MakeRepository(VegaDbContext vegaDbContext) : base(vegaDbContext)
        {
            _vegaDbContext = vegaDbContext;
        }

        public async Task<IEnumerable<Make>> GetWithRelated()
        {
            return await _vegaDbContext.Makes.Include(mk => mk.Models).ToListAsync();
        }
    }
}
