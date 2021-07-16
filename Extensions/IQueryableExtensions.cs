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
    }
}