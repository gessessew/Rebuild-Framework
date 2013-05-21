
using System.Collections.Generic;
namespace Rebuild.Extensions
{
    public static class Int32Extensions
    {
        public static int Clamp(this int value, int minValue, int maxValue)
        {
            return value < minValue ? minValue : value > maxValue ? maxValue : value;
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
