using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AssertEqual<TKey, TValue>(this Dictionary<TKey, TValue> actual, Dictionary<TKey, TValue> expected)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.IsTrue(expected.Keys.OrderBy(k => k).SequenceEqual(actual.Keys.OrderBy(k => k)));
            Assert.AreSame(expected.Comparer, actual.Comparer);

            foreach (var kv in actual)
            {
                Assert.AreEqual(expected[kv.Key], kv.Value);
            }
        }
    }
}
