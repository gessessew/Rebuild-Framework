using System;

namespace Rebuild.Extensions
{
    public static class DoubleExtensions
    {
        public static double Clamp(this double value, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(value, minValue), maxValue);
        }

        public static double ClamMax(this double value, double maxValue)
        {
            return Math.Min(value, maxValue);
        }

        public static double ClampMin(this double value, double minValue)
        {
            return Math.Max(value, minValue);
        }

        public static bool IsAlmostEqualsTo(this double d, double compareValue, double epsilon = double.Epsilon)
        {
            return Math.Abs(d - compareValue) <= epsilon;
        }
    }
}
