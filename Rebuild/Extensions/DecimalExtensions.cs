using System;

namespace Rebuild.Extensions
{
    public static class DecimalExtensions
    {
        public static bool IsAlmostEqualsTo(this decimal d1, decimal d2, decimal epsilon = 0)
        {
            return Math.Abs(d1 - d2) <= epsilon;
        }
    }
}
