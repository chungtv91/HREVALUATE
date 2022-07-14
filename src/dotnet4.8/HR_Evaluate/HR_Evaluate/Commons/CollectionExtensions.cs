using System.Collections.Generic;

namespace HR_Evaluate.Commons
{
    public static class CollectionExtensions
    {
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