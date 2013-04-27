using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AssertSequenceEqual<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer = null)
        {
            Assert.IsTrue(actual.SequenceEqual(expected, comparer));
        }

        public static void AssertSequenceEqual<T>(this IEnumerable<T> actual, params T[] expected)
        {
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        public static void AssertSequenceEqual<T>(this IEnumerable<T> actual, IEqualityComparer<T> comparer, params T[] expected)
        {
            Assert.IsTrue(actual.SequenceEqual(expected, comparer));
        }
    }
}
