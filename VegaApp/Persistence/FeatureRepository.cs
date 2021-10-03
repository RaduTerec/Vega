using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;

namespace Vega.Persistence
{
    public class FeatureRepository : Repository<Feature>
    {
        public FeatureRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
