using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HRE.Core.Extensions
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection, Func<T, object> keySelector)
        {
            List<T> sorted = collection.OrderBy(keySelector).ToList();
            for (int i = 0; i < sorted.Count; i++)
            {
                collection.Move(collection.IndexOf(sorted[i]), i);
            }
        }

        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">Item to check and add</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        /// <summary>
        /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
        /// </summary>
        public static IEnumerable<T> PageBy<T>(this IEnumerable<T> query, int skipCount, int maxResultCount)
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
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate)
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
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, int, bool> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static IEnumerable<TSource> WhereIIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> truePredicate, Func<TSource, bool> falsePredicate)
        {
            if (condition) return source.Where(truePredicate);
            return source.Where(falsePredicate);
        }

        public static IEnumerable<T> WhereIIf<T>(this IEnumerable<T> query, bool condition, Func<T, int, bool> truePredicate, Func<T, int, bool> falsePredicate)
        {
            return condition ? query.Where(truePredicate) : query.Where(falsePredicate);
        }

        public static bool ContainsAll<T>(this List<T> searchItems, params T[] numbers) where T : struct, IComparable, IFormattable, IConvertible => numbers.All(searchItems.Contains);

        public static T ItemAt<T>(this List<T> items, int index)
            where T : class, new()
        {
            if (items is null or { Count: 0 })
            {
                return default;
            }

            if (items.Count <= index) return default;
            return items[index];
        }

        public static string DefaultEmptyIfNull(this string text) => text == null ? "" : text;

        public static T ItemAt1<T>(this List<T> items, int index)
            where T : class, new()
        {
            if (items is null or { Count: 0 })
            {
                return default;
            }

            if (items.Count <= index) return default;
            return items[index];
        }

        public static string DefaultEmptyIfScoreNull(this string scorenumber) => scorenumber == null ? "0" : scorenumber;
    }
}
