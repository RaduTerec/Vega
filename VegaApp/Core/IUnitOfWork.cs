using System.Threading.Tasks;
using Vega.Core.Repositories;

namespace Vega.Core
{
    public interface IUnitOfWork
    {
        IVehicleRepository Vehicles { get; }
        IPhotoRepository Photos { get; }
        public Task<int> Complete();
    }
}