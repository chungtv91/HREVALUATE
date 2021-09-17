using System;
using System.Linq;
using System.Linq.Expressions;

namespace HRE.Core.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
        /// </summary>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.Skip(skipCount).Take(maxResultCount);
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        /// <summary>
        /// Filters a <see cref="IQueryable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <paramref name="condition"/></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static IQueryable<TSource> WhereIIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> truePredicate, Expression<Func<TSource, bool>> falsePredicate)
        {
            if (condition) return source.Where(truePredicate);
            return source.Where(falsePredicate);
        }

        public static IQueryable<T> WhereIIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> truePredicate, Expression<Func<T, int, bool>> falsePredicate)
        {
            return condition ? query.Where(truePredicate) : query.Where(falsePredicate);
        }
    }
}
