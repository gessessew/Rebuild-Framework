using System;

namespace Rebuild.Utils
{
    public static class DateTimes
    {
        public static DateTime Max(DateTime d1, DateTime d2)
        {
            return d1 > d2 ? d1 : d2;
        }

        public static DateTimeOffset Max(DateTimeOffset d1, DateTimeOffset d2)
        {
            return d1 > d2 ? d1 : d2;
        }

        public static DateTime Min(DateTime d1, DateTime d2)
        {
            return d1 < d2 ? d1 : d2;
        }

        public static DateTimeOffset Min(DateTimeOffset d1, DateTimeOffset d2)
        {
            return d1 < d2 ? d1 : d2;
        }
    }
}
