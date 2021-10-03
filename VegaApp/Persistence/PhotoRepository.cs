using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Core.Models;
using Vega.Core.Repositories;

namespace Vega.Persistence
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly VegaDbContext _vegaDbContext;
        public PhotoRepository(VegaDbContext vegaDbContext) : base(vegaDbContext)
        {
            _vegaDbContext = vegaDbContext;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await _vegaDbContext.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }
    }
}