using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Vaelastrasz.Library.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySearch<T>(this IQueryable<T> queryable, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return queryable;

            return queryable;
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            return queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> queryable, string? sortBy) where T : class
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return queryable;

            var allowedProperties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var sortExpressions = new List<string>();

            foreach (var part in sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                var tokens = part.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 0 || !allowedProperties.Contains(tokens[0]))
                    continue;

                var direction = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? "descending"
                    : "ascending";

                sortExpressions.Add($"{tokens[0]} {direction}");
            }

            return sortExpressions.Count > 0
                ? queryable.OrderBy(string.Join(", ", sortExpressions))
                : queryable;
        }
    }
}
