using System;

namespace Rebuild.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Age(this DateTime date, DateTime currentTime)
        {
            return new DateTime((currentTime - date).Ticks).AddYears(-1).AddDays(-1);
        }

        public static DateTime Clamp(this DateTime value, DateTime minValue, DateTime maxValue)
        {
            return value < minValue ? minValue : value > maxValue ? maxValue : value;
        }

        public static DateTimeOffset DateTimeOffset(this DateTime value, TimeSpan offset = default(TimeSpan))
        {
            return new DateTimeOffset(value, offset);
        }

        public static DateTimeOffset DateTimeOffsetUtc(this DateTime value)
        {
            return new DateTimeOffset(value.ToUniversalTime());
        }

        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return value.AddDays(-value.Day + 1);
        }

        public static DateTime FirstDayOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1);
        }

        public static DateTime FloorToYears(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime FloorToMonths(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime FloorToHours(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, date.Kind);
        }

        public static DateTime FloorToMinutes(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, date.Kind);
        }

        public static DateTime FloorToSeconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        }

        public static DateTime FloorToMilliseconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, date.Kind);
        }

        public static bool HasValue(this DateTime date)
        {
            return date != default(DateTime);
        }

        public static bool HasValue(this DateTime? date)
        {
            return date.GetValueOrDefault() != default(DateTime);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return value.AddDays(-value.Day).AddMonths(1);
        }

        public static DateTime LastDayOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 12, 31);
        }

        public static DateTime NextDay(this DateTime value, DayOfWeek dayOfWeek)
        {
            var v = (int)value.DayOfWeek - (int)dayOfWeek;
            return value.AddDays(v >= 0 ? 7 - v : -v);
        }

        public static DateTime PreviousDay(this DateTime value, DayOfWeek dayOfWeek)
        {
            var v = (int)dayOfWeek - (int)value.DayOfWeek;
            return value.AddDays(v >= 0 ? v - 7 : v);
        }

        public static int WeekNumber(this DateTime value, bool excludeLastYear = true, DayOfWeek startWeekDay = DayOfWeek.Sunday)
        {
            var d = value.FirstDayOfYear();
            excludeLastYear &= d.DayOfWeek != startWeekDay;

            if (d.DayOfWeek != startWeekDay)
                d = d.PreviousDay(startWeekDay);

            return (int)((value.Date - d).TotalDays / 7) + (excludeLastYear ? 1 : 0);
        }
    }
}
