using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Core.Models;
using Vega.Extensions;

namespace vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Vehicle> ApplyFiltering(this IQueryable<Vehicle> query, VehicleQuery queryObj)
        {
            if (queryObj.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);
            }

            return query;
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject sortQuery, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrEmpty(sortQuery.SortBy) || !columnsMap.ContainsKey(sortQuery.SortBy))
            {
                return query;
            }

            if (sortQuery.IsAscending)
            {
                return query.OrderBy(columnsMap[sortQuery.SortBy]);
            }
            else
            {
                return query.OrderByDescending(columnsMap[sortQuery.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.Page <= 0)
            {
                queryObj.Page = 1;
            }

            if (queryObj.PageSize <= 0)
            {
                queryObj.PageSize = 10;
            }

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}