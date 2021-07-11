using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core
{
    public interface IVehicleRepository
    {
        public Task<Vehicle> Get(long id);
        public Task<Vehicle> GetWithRelated(long id);
        public Task<Vehicle> GetWithFeatures(long id);
        public Task<IEnumerable<Vehicle>> GetAll(VehicleQuery filter);
        public Task AddAsync(Vehicle vehicle);
        public void Remove(Vehicle vehicle);
    }
}