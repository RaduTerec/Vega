using System.Threading.Tasks;

namespace Vega.Core
{
    public interface IUnitOfWork
    {
        public Task<int> Complete();
    }
}