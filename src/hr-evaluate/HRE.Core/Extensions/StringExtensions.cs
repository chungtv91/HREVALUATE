using System;
using System.Linq;
using System.Text.Json;

namespace HRE.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly string[] TrueValues = { "TRUE", "T", "Y", "1" };

        public static bool AsBool(this string text)
        {
            return TrueValues.Contains(text, StringComparer.OrdinalIgnoreCase);
        }

        public static T AsEnum<T>(this string text) where T : struct
        {
            if (Enum.TryParse<T>(text, true, out T result))
            {
                return result;
            }

            return default;
        }

        public static T? AsEnumNullable<T>(this string? text) where T : struct
        {
            if (text != null && Enum.TryParse<T>(text, true, out T result))
            {
                return result;
            }

            return default;
        }

        public static Guid AsGuid(this string text)
        {
            if (Guid.TryParse(text, out var x)) return x;
            return default;
        }

        public static Guid? AsGuidNullable(this string? text)
        {
            if (text != null && Guid.TryParse(text, out var result))
            {
                return result;
            }

            return default;
        }

        public static byte AsByte(this string text)
        {
            if (byte.TryParse(text, out var x)) return x;
            return default;
        }

        public static int? AsByteNullable(this string? text)
        {
            if (text != null && byte.TryParse(text, out var result))
            {
                return result;
            }

            return default;
        }

        public static int AsInt32(this string text)
        {
            if (int.TryParse(text, out var x)) return x;
            return default;
        }

        public static int? AsInt32Nullable(this string? text)
        {
            if (text != null && int.TryParse(text, out var result))
            {
                return result;
            }

            return default;
        }

        public static long AsInt64(this string text)
        {
            if (long.TryParse(text, out var x)) return x;
            return default;
        }

        public static long? AsInt64Nullable(this string? text)
        {
            if (text != null && long.TryParse(text, out var result))
            {
                return result;
            }

            return default;
        }

        public static double AsDouble(this string text)
        {
            if (double.TryParse(text, out var x)) return x;
            return default;
        }

        public static double? AsDoubleNullable(this string? text)
        {
            if (text != null && double.TryParse(text, out var result))
            {
                return result;
            }

            return default;
        }

        public static decimal AsDecimal(this string text)
        {
            if (decimal.TryParse(text, out var x)) return x;
            return default;
        }

        public static decimal? AsDecimalNullable(this string? text)
        {
            if (text != null && decimal.TryParse(text, out var result))
            {
                return result;
            }

            return default;
        }

        public static DateTime AsDateTime(this string text)
        {
            if (DateTime.TryParse(text, out var x)) return x;
            return default;
        }

        public static DateTime? AsDateTimeNullable(this string? text)
        {
            if (text != null && DateTime.TryParse(text, out var result))
            {
                return result;
            }

            return default;
        }

        public static T? AsObjectFromJson<T>(this string? jsonText)
        {
            if (string.IsNullOrEmpty(jsonText)) return default;
            return JsonSerializer.Deserialize<T>(jsonText);
        }

        public static string DefaultIfNull(this string? text, string defaultText = "N/A")
        {
            if (text == null) return defaultText;
            return text;
        }

        /// <summary>
        /// Lấy ra thời gian để hiển thị cho người dùng.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToShortTime(this DateTime time)
        {
            if (time.Date == DateTime.Today) return time.ToString("H:mm");
            return time.ToString("M/d/yyyy H:mmm");
        }

        public static string DefaultIfNullOrEmpty(this string? text, string defaultText = "N/A")
        {
            if (string.IsNullOrEmpty(text)) return defaultText;
            return text;
        }

        public static string DefaultIfNullOrWhiteSpace(this string? text, string defaultText = "N/A")
        {
            if (string.IsNullOrWhiteSpace(text)) return defaultText;
            return text;
        }

        public static bool ContainsAll(this string searchText, params string[] texts) => texts.All(searchText.Contains);

        public static bool ContainsAny(this string text, params string[] items) => items.Any(text.Contains);
    }
}
