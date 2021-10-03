using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core.Repositories
{
    public interface IPhotoRepository  : IRepository<Photo>
    {
         Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}