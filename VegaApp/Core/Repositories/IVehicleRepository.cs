using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        public Task<Vehicle> GetWithRelated(long id);
        public Task<Vehicle> GetWithFeatures(long id);
        public Task<QueryResult<Vehicle>> QueryAll(VehicleQuery vehicleQuery);
    }
}