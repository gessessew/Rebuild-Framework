using System;
using System.Globalization;

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
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime FirstDayOfWeek(this DateTime value, DayOfWeek firstDayOfWeek = DayOfWeek.Sunday)
        {
            value = value.Date;
            return value.DayOfWeek == firstDayOfWeek ? value : value.PreviousDay(firstDayOfWeek);
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
            return value.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime LastDayOfWeek(this DateTime value, DayOfWeek lastDayOfWeek = DayOfWeek.Sunday)
        {
            value = value.Date;
            return value.DayOfWeek == lastDayOfWeek ? value : value.NextDay(lastDayOfWeek);
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

        public static long ToJavascriptMilliseconds(this DateTime value)
        {
            return (long)(value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        public static int WeekOfYear(this DateTime value, CalendarWeekRule rule = CalendarWeekRule.FirstDay, DayOfWeek firstDayOfWeek = DayOfWeek.Sunday)
        {
            return CultureInfo
                .CurrentCulture
                .DateTimeFormat
                .Calendar
                .GetWeekOfYear(value, rule, firstDayOfWeek);
        }
    }
}
