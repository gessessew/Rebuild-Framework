using Rebuild.Utils;
using System;

namespace Rebuild.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTime Age(this DateTimeOffset date, DateTimeOffset currentTime)
        {
            return new DateTime((currentTime - date).Ticks).AddYears(-1).AddDays(-1);
        }

        public static DateTimeOffset Clamp(this DateTimeOffset value, DateTimeOffset minValue, DateTimeOffset maxValue)
        {
            return DateTimes.Min(DateTimes.Max(value, minValue), maxValue);
        }

        public static DateTimeOffset ClamMax(this DateTimeOffset value, DateTimeOffset maxValue)
        {
            return DateTimes.Min(value, maxValue);
        }

        public static DateTimeOffset ClampMin(this DateTimeOffset value, DateTimeOffset minValue)
        {
            return DateTimes.Max(value, minValue);
        }

        public static bool HasValue(this DateTimeOffset date)
        {
            return date != default(DateTimeOffset);
        }

        public static bool HasValue(this DateTimeOffset? date)
        {
            return date.GetValueOrDefault().HasValue();
        }

        public static DateTime? ToUtc(this DateTimeOffset? date)
        {
            return date == null ? (DateTime?)null : date.Value.UtcDateTime;
        }
    }
}
