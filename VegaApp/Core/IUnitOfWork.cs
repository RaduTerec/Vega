using System.Threading.Tasks;
using Vega.Core.Models;
using Vega.Core.Repositories;

namespace Vega.Core
{
    public interface IUnitOfWork
    {
        IVehicleRepository Vehicles { get; }
        IPhotoRepository Photos { get; }
        IRepository<Feature> Features { get; }
        IMakeRepository Makes { get; }
        public Task<int> Complete();
    }
}