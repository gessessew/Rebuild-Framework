using Rebuild.Utils;
using System;
using System.Globalization;

namespace Rebuild.Extensions
{
    public static class DateTimeExtensions
    {
        public static Age Age(this DateTime date, DateTime currentDate)
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

        public static DateTime Clamp(this DateTime value, DateTime minValue, DateTime maxValue)
        {
            return DateTimes.Min(DateTimes.Max(value, minValue), maxValue);
        }

        public static DateTime ClamMax(this DateTime value, DateTime maxValue)
        {
            return DateTimes.Min(value, maxValue);
        }

        public static DateTime ClampMin(this DateTime value, DateTime minValue)
        {
            return DateTimes.Max(value, minValue);
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

        public static bool HasValue(this DateTime date)
        {
            return date != default(DateTime);
        }

        public static bool HasValue(this DateTime? date)
        {
            return date.GetValueOrDefault() != default(DateTime);
        }

        public static bool IsWeekend(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;
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

        public static DateTime NextDayOrValue(this DateTime value, DayOfWeek dayOfWeek)
        {
            return value.DayOfWeek == dayOfWeek ? value : value.NextDay(dayOfWeek);
        }

        public static DateTime PreviousDay(this DateTime value, DayOfWeek dayOfWeek)
        {
            var v = (int)dayOfWeek - (int)value.DayOfWeek;
            return value.AddDays(v >= 0 ? v - 7 : v);
        }

        public static DateTime PreviousDayOrValue(this DateTime value, DayOfWeek dayOfWeek)
        {
            return value.DayOfWeek == dayOfWeek ? value : value.PreviousDay(dayOfWeek);
        }

        public static long ToJavascriptMilliseconds(this DateTime value)
        {
            return (long)(value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        public static long ToUnixTimestamp(this DateTime date)
        {
            return (long)(date - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
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
