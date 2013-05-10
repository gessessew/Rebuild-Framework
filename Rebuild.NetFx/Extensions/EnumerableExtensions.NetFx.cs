using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Rebuild.Extensions
{
    public static partial class EnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(items, comparer);
        }

        public static NameValueCollection ToNameValueCollection<T>(this IEnumerable<T> items, Func<T, string> nameSelector, Func<T, string> valueSelector, IEqualityComparer<string> comparer = null)
        {
            var nvc = new NameValueCollection((comparer ?? EqualityComparer<string>.Default).ToUnTypedEqualityComparer());

            foreach (var item in items)
            {
                nvc[nameSelector(item)] = valueSelector(item);
            }

            return nvc;
        }
    }
}
