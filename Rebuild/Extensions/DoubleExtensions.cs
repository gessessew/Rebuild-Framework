using System;

namespace Rebuild.Extensions
{
    public static class DoubleExtensions
    {
        public static double Clamp(this double d, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(d, minValue), maxValue);
        }

        public static bool IsAlmostEqualsTo(this double d, double compareValue, double epsilon = double.Epsilon)
        {
            return Math.Abs(d - compareValue) <= epsilon;
        }
    }
}
