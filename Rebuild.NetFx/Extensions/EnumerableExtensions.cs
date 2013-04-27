using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class EnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(items, comparer);
        }
    }
}
