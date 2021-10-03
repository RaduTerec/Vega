using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core.Repositories
{
    public interface IVehicleRepository
    {
        public Task<Vehicle> Get(long id);
        public Task<Vehicle> GetWithRelated(long id);
        public Task<Vehicle> GetWithFeatures(long id);
        public Task<QueryResult<Vehicle>> GetAll(VehicleQuery vehicleQuery);
        public Task AddAsync(Vehicle vehicle);
        public void Remove(Vehicle vehicle);
    }
}