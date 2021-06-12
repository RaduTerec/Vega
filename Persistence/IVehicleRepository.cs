using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Persistence
{
    public interface IVehicleRepository
    {
        public Task<Vehicle> GetVehicle(long id);
        public Task<IEnumerable<Vehicle>> GetVehicles();
    }
}