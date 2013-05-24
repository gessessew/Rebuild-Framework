using System;

namespace Rebuild.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal Clamp(this decimal d, decimal minValue, decimal maxValue)
        {
            return Math.Min(Math.Max(d, minValue), maxValue);
        }

        public static decimal ClamMax(this decimal value, decimal maxValue)
        {
            return Math.Min(value, maxValue);
        }

        public static decimal ClampMin(this decimal value, decimal minValue)
        {
            return Math.Max(value, minValue);
        }

        public static bool IsAlmostEqualsTo(this decimal d1, decimal d2, decimal epsilon = 0)
        {
            return Math.Abs(d1 - d2) <= epsilon;
        }
    }
}
