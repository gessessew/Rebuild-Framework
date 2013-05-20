using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Rebuild.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Provide an empty string if the specified string is null.
        /// </summary>
        public static string EmptyIfNull(this string s)
        {
            return s ?? string.Empty;
        }

        public static bool EndsWithCulture(this string s, string value)
        {
            return s.EndsWith(value, StringComparison.CurrentCulture);
        }

        public static bool EndsWithCultureIgnoreCase(this string s, string value)
        {
            return s.EndsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool EndsWithIgnoreCase(this string s, string value)
        {
            return s.EndsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EqualsAnyIgnoreCase(this string s, IEnumerable<string> args)
        {
            return s.EqualsAny(args, StringComparer.OrdinalIgnoreCase);
        }

        public static bool EqualsAnyIgnoreCase(this string s, params string[] args)
        {
            return s.EqualsAny(StringComparer.OrdinalIgnoreCase, args);
        }

        public static bool EqualsAnyIgnoreCase(this string s, string s0, string s1)
        {
            return s.EqualsAny(s0, s1, StringComparer.OrdinalIgnoreCase);
        }

        public static bool EqualsAnyIgnoreCase(this string s, string s0, string s1, string s2)
        {
            return s.EqualsAny(s0, s1, s2, StringComparer.OrdinalIgnoreCase);
        }

        public static bool EqualsAnyIgnoreCase(this string s, string s0, string s1, string s2, string s3)
        {
            return s.EqualsAny(s0, s1, s2, s3, StringComparer.OrdinalIgnoreCase);
        }

        public static bool EqualsCulture(this string s, string other)
        {
            return string.Equals(s, other, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool EqualsCultureIgnoreCase(this string s, string other)
        {
            return string.Equals(s, other, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool EqualsIgnoreCase(this string s, string other)
        {
            return string.Equals(s, other, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EqualsOrdinal(this string s, string other)
        {
            return string.Equals(s, other, StringComparison.Ordinal);
        }

        public static bool HasValue(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static int IndexOfCulture(this string s, string value, int startIndex = 0, int count = int.MaxValue)
        {
            return s.IndexOfInternal(value, startIndex, count, StringComparison.CurrentCulture);
        }

        public static int IndexOfCultureIgnoreCase(this string s, string value, int startIndex = 0, int count = int.MaxValue)
        {
            return s.IndexOfInternal(value, startIndex, count, StringComparison.CurrentCultureIgnoreCase);
        }

        public static int IndexOfIgnoreCase(this string s, string value, int startIndex = 0, int count = int.MaxValue)
        {
            return s.IndexOfInternal(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
        }

        private static int IndexOfInternal(this string s, string value, int startIndex, int count, StringComparison comparison)
        {
            startIndex = Math.Max(Math.Min(startIndex, s.Length - 1), 0);
            count = Math.Max(Math.Min(count, s.Length - startIndex), 0);

            return count == 0 ? -1 : s.IndexOf(value, startIndex, count, comparison);
        }

        public static T IfHasValue<T>(this string s, Func<string, T> selector, T defaultValue = default(T))
        {
            return s.HasValue() ? selector(s) : defaultValue;
        }

        public static string IfNoValue(this string s, Func<string> provider)
        {
            return s.HasValue() ? s : provider();
        }

        public static string IfNoValue(this string s, string defaultValue = null)
        {
            return s.HasValue() ? s : defaultValue;
        }

        public static int LastIndexOfCulture(this string s, string value, int startIndex = 0, int count = int.MaxValue)
        {
            return s.LastIndexOfInternal(value, startIndex, count, StringComparison.CurrentCulture);
        }

        public static int LastIndexOfCultureIgnoreCase(this string s, string value, int startIndex = 0, int count = int.MaxValue)
        {
            return s.LastIndexOfInternal(value, startIndex, count, StringComparison.CurrentCultureIgnoreCase);
        }

        public static int LastIndexOfIgnoreCase(this string s, string value, int startIndex = 0, int count = int.MaxValue)
        {
            return s.LastIndexOfInternal(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
        }

        private static int LastIndexOfInternal(this string s, string value, int startIndex, int count, StringComparison comparison)
        {
            startIndex = Math.Max(Math.Min(startIndex, s.Length - 1), 0);
            count = Math.Max(Math.Min(count, s.Length - startIndex), 0);

            return count == 0 ? -1 : s.LastIndexOf(value, startIndex, count, comparison);
        }

        public static string Left(this string s, int maxLength)
        {
            return s.Substring(0, Math.Min(s.Length, maxLength));
        }

        public static string Replace(this string s, string oldValue, string newValue, StringComparison comparisonType)
        {
            var startIndex = 0;

            while (true)
            {
                startIndex = s.IndexOf(oldValue, startIndex, comparisonType);

                if (startIndex < 0)
                {
                    break;
                }

                s = string.Concat(s.Substring(0, startIndex), newValue, s.Substring(startIndex + oldValue.Length));
                startIndex += newValue.Length;
            }

            return s;
        }

        public static string ReplaceIgnoreCase(this string s, string oldValue, string newValue, StringComparison comparisonType)
        {
            return s.Replace(oldValue, newValue, StringComparison.OrdinalIgnoreCase);
        }

        public static string Right(this string s, int maxLength)
        {
            return s.Substring(Math.Max(s.Length - maxLength, 0));
        }

        public static bool StartsWithCulture(this string s, string value)
        {
            return s.StartsWith(value, StringComparison.CurrentCulture);
        }

        public static bool StartsWithCultureIgnoreCase(this string s, string value)
        {
            return s.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool StartsWithIgnoreCase(this string s, string value)
        {
            return s.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        public static string Substr(this string s, int startIndex, int endIndex)
        {
            return s.Substring(startIndex, endIndex - startIndex);
        }

        public static byte[] ToBase64Bytes(this string s)
        {
            return Convert.FromBase64String(s);
        }

        public static bool ToBoolean(this string s, bool defaultValue = false)
        {
            bool v;
            return bool.TryParse(s, out v) ? v : defaultValue;
        }

        public static bool? ToBooleanNull(this string s, bool? defaultValue = null)
        {
            bool v;
            return bool.TryParse(s, out v) ? v : defaultValue;
        }

        public static byte[] ToBytes(this string s, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetBytes(s);
        }

        public static DateTime ToDateTime(this string s, DateTime defaultValue = default(DateTime), IFormatProvider provider = null, DateTimeStyles style = DateTimeStyles.None)
        {
            DateTime d;
            return DateTime.TryParse(s, provider ?? CultureInfo.InvariantCulture, style, out d) ? d : defaultValue;
        }

        public static DateTime? ToDateTimeNull(this string s, DateTime? defaultValue = null, IFormatProvider provider = null, DateTimeStyles style = DateTimeStyles.None)
        {
            DateTime d;
            return DateTime.TryParse(s, provider ?? CultureInfo.InvariantCulture, style, out d) ? d : defaultValue;
        }

        public static DateTimeOffset ToDateTimeOffset(this string s, DateTimeOffset defaultValue = default(DateTimeOffset), IFormatProvider provider = null, DateTimeStyles style = DateTimeStyles.None)
        {
            DateTimeOffset d;
            return DateTimeOffset.TryParse(s, provider ?? CultureInfo.InvariantCulture, style, out d) ? d : defaultValue;
        }

        public static DateTimeOffset? ToDateTimeOffsetNull(this string s, DateTimeOffset? defaultValue = null, IFormatProvider provider = null, DateTimeStyles style = DateTimeStyles.None)
        {
            DateTimeOffset d;
            return DateTimeOffset.TryParse(s, provider ?? CultureInfo.InvariantCulture, style, out d) ? d : defaultValue;
        }

        public static decimal ToDecimal(this string s, decimal defaultValue = 0, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            decimal d;
            return decimal.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out d) ? d : defaultValue;
        }

        public static decimal? ToDecimalNull(this string s, decimal? defaultValue = null, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            decimal d;
            return decimal.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out d) ? d : defaultValue;
        }

        public static double ToDouble(this string s, double defaultValue = 0, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            double d;
            return double.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out d) ? d : defaultValue;
        }

        public static double? ToDoubleNull(this string s, double? defaultValue = null, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            double d;
            return double.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out d) ? d : defaultValue;
        }

        public static Guid ToGuid(this string s, Guid defaultValue = default(Guid))
        {
            Guid g;
            return Guid.TryParse(s, out g) ? g : defaultValue;
        }

        public static Guid ToGuid(this string s, Func<Guid> selector)
        {
            Guid g;
            return Guid.TryParse(s, out g) ? g : selector();
        }

        public static Guid ToGuid(this string s, Func<string, Guid> selector)
        {
            Guid g;
            return Guid.TryParse(s, out g) ? g : selector(s);
        }

        public static Guid? ToGuidNull(this string s, Guid? defaultValue = null)
        {
            Guid g;
            return Guid.TryParse(s, out g) ? g : defaultValue;
        }

        public static int ToInt32(this string s, int defaultValue = 0, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            int i;
            return int.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out i) ? i : defaultValue;
        }

        public static int? ToInt32Null(this string s, int? defaultValue = null, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            int i;
            return int.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out i) ? i : defaultValue;
        }

        public static long ToInt64(this string s, long defaultValue = 0, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            long l;
            return long.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out l) ? l : defaultValue;
        }

        public static long? ToInt64Null(this string s, long? defaultValue = null, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            long l;
            return long.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out l) ? l : defaultValue;
        }

        public static float ToSingle(this string s, float defaultValue = 0, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            float f;
            return float.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out f) ? f : defaultValue;
        }

        public static float? ToSingleNull(this string s, float? defaultValue = null, IFormatProvider provider = null, NumberStyles style = NumberStyles.Any)
        {
            float f;
            return float.TryParse(s, style, provider ?? CultureInfo.InvariantCulture, out f) ? f : defaultValue;
        }

        public static TimeSpan ToTimeSpan(this string s, TimeSpan defaultValue = default(TimeSpan), IFormatProvider provider = null)
        {
            TimeSpan t;
            return TimeSpan.TryParse(s, provider ?? CultureInfo.InvariantCulture, out t) ? t : defaultValue;
        }

        public static TimeSpan? ToTimeSpanNull(this string s, TimeSpan? defaultValue = null, IFormatProvider provider = null)
        {
            TimeSpan t;
            return TimeSpan.TryParse(s, provider ?? CultureInfo.InvariantCulture, out t) ? t : defaultValue;
        }
    }
}
