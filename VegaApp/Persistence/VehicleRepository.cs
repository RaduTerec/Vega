using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vega.Extensions;
using Vega.Core.Models;
using Vega.Core.Repositories;

namespace Vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _vegaDbContext;

        public VehicleRepository(VegaDbContext vegaDbContext)
        {
            _vegaDbContext = vegaDbContext;
        }

        /// <summary>
        /// Get a vehicle based on <paramref name="id"/> with related features, 
        /// make and model from the context
        /// </summary>
        /// <param name="id">id of the <see cref="Vehicle"/> to retrieve</param>
        /// <returns><see cref="Vehicle"/> object</returns>
        public async Task<Vehicle> GetWithRelated(long id)
        {
            return await _vegaDbContext.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id.Equals(id));
        }

        /// <summary>
        /// Get a vehicle based on <paramref name="id"/> with related features 
        /// from the context
        /// </summary>
        /// <param name="id">id of the <see cref="Vehicle"/> to retrieve</param>
        /// <returns><see cref="Vehicle"/> object</returns>
        public async Task<Vehicle> GetWithFeatures(long id)
        {
            return await _vegaDbContext.Vehicles
                    .Include(v => v.Features)
                    .SingleOrDefaultAsync(v => v.Id.Equals(id));
        }

        /// <summary>
        /// Get a vehicle based on <paramref name="id"/> from the context.
        /// </summary>
        /// <param name="id">if of the <see cref="Vehicle"/> to retrieve</param>
        /// <returns><see cref="Vehicle"/> object</returns>
        public async Task<Vehicle> Get(long id)
        {
            return await _vegaDbContext.Vehicles.FindAsync(id);
        }

        /// <summary>
        /// Gets an enumerable of vehicles
        /// </summary>
        /// <param name="vehicleQuery">Filtering and sorting query for vehicles</param>
        /// <returns><see cref="QueryResult{Vehicle}"/></returns>
        public async Task<QueryResult<Vehicle>> GetAll(VehicleQuery vehicleQuery)
        {
            var result = new QueryResult<Vehicle>();

            var query = _vegaDbContext.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();

            if (vehicleQuery.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeId == vehicleQuery.MakeId);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };

            query = query.ApplyOrdering(vehicleQuery, columnsMap);
            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(vehicleQuery);
            result.Items = await query.ToListAsync();

            return result;
        }

        /// <summary>
        /// Add a <paramref name="vehicle"/> to the context.
        /// </summary>
        /// <param name="vehicle"><see cref="Vehicle"/> to be added</param>
        public async Task AddAsync(Vehicle vehicle)
        {
            await _vegaDbContext.Vehicles.AddAsync(vehicle);
        }

        /// <summary>
        /// Removes a <paramref name="vehicle"/> from the context.
        /// </summary>
        /// <param name="vehicle"><see cref="Vehicle"/></param>
        public void Remove(Vehicle vehicle)
        {
            _vegaDbContext.Vehicles.Remove(vehicle);
        }
    }
}