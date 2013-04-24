using System;

namespace Rebuild.Extensions
{
    public static class FloatExtensions
    {
        public static bool IsAlmostEqualsTo(this float f, float compareValue, float epsilon = float.Epsilon)
        {
            return Math.Abs(f - compareValue) <= epsilon;
        }
    }
}
