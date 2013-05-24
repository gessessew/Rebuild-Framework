using System;

namespace Rebuild.Extensions
{
    public static class FloatExtensions
    {
        public static float Clamp(this float value, float minValue, float maxValue)
        {
            return Math.Min(Math.Max(value, minValue), maxValue);
        }

        public static float ClamMax(this float value, float maxValue)
        {
            return Math.Min(value, maxValue);
        }

        public static float ClampMin(this float value, float minValue)
        {
            return Math.Max(value, minValue);
        }

        public static bool IsAlmostEqualsTo(this float f, float compareValue, float epsilon = float.Epsilon)
        {
            return Math.Abs(f - compareValue) <= epsilon;
        }
    }
}
