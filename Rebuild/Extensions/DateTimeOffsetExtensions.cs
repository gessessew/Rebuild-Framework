using Rebuild.Utils;
using System;

namespace Rebuild.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static Age Age(this DateTimeOffset date, DateTimeOffset currentDate)
        {
            var time = currentDate.TimeOfDay - date.TimeOfDay;

            if (time < TimeSpan.Zero)
            {
                time += TimeSpan.FromHours(24);
                currentDate = currentDate.AddDays(-1);
            }

            var day = currentDate.Day - date.Day;
            if (day < 0)
            {
                currentDate = currentDate.AddDays(-currentDate.Day - 1);
                day += currentDate.Day;
            }

            var month = currentDate.Month - date.Month;
            if (month < 0)
            {
                currentDate = currentDate.AddYears(-1);
                month += 12;
            }

            return new Age(currentDate.Year - date.Year, month, day, time);
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
