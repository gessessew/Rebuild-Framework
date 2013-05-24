using System;

namespace Rebuild.Extensions
{
    public static class Int64Extensions
    {
        public static long Clamp(this long value, long minValue, long maxValue)
        {
            return Math.Min(Math.Max(value, minValue), maxValue);
        }

        public static long ClamMax(this long value, long maxValue)
        {
            return Math.Min(value, maxValue);
        }

        public static long ClampMin(this long value, long minValue)
        {
            return Math.Max(value, minValue);
        }
    }
}
