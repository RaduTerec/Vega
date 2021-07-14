using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Extensions;

namespace vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, ISortQuery sortQuery, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrEmpty(sortQuery.SortBy) || !columnsMap.ContainsKey(sortQuery.SortBy.ToLowerInvariant()))
            {
                return query;
            }

            string sortBy = sortQuery.SortBy.ToLowerInvariant();

            if (sortQuery.IsAscending)
            {
                return query.OrderBy(columnsMap[sortBy]);
            }
            else
            {
                return query.OrderByDescending(columnsMap[sortBy]);
            }
        }
    }
}