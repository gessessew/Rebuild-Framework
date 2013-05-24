using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class Int32Extensions
    {
        public static int Clamp(this int value, int minValue, int maxValue)
        {
            return Math.Min(Math.Max(value, minValue), maxValue);
        }

        public static int ClamMax(this int value, int maxValue)
        {
            return Math.Min(value, maxValue);
        }

        public static int ClampMin(this int value, int minValue)
        {
            return Math.Max(value, minValue);
        }

        public static int CombineHash<T>(this int hash, T value, IEqualityComparer<T> comparer = null)
        {
            if (value == null)
            {
                return hash;
            }

            var v = comparer == null ? value.GetHashCode() : comparer.GetHashCode(value);

            unchecked
            {
                return hash * 31 + v;
            }
        }
    }
}
