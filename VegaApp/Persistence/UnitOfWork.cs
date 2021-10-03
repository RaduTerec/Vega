using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core;
using Vega.Core.Models;
using Vega.Core.Repositories;

namespace Vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext _vegaDbContext;
        public UnitOfWork(VegaDbContext vegaDbContext)
        {
            _vegaDbContext = vegaDbContext;
            Vehicles = new VehicleRepository(_vegaDbContext);
            Photos = new PhotoRepository(_vegaDbContext);
            Features = new FeatureRepository(_vegaDbContext);
            Makes = new MakeRepository(_vegaDbContext);
        }

        public IVehicleRepository Vehicles { get; }
        public IPhotoRepository Photos { get; }
        public IRepository<Feature> Features { get; }
        public IMakeRepository Makes { get; }

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