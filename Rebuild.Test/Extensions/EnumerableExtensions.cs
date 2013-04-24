using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AssertSequenceEquals<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer = null)
        {
            Assert.IsTrue(actual.SequenceEqual(expected, comparer));
        }
    }
}
