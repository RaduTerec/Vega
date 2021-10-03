using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core.Repositories
{
    public interface IMakeRepository  : IRepository<Make>
    {
        public Task<IEnumerable<Make>> GetWithRelated();

    }
}
