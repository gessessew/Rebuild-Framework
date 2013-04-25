using System;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, params T[] args)
        {
            return items.Concat((IEnumerable<T>)args);
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> items)
        {
            return items ?? Enumerable.Empty<T>();
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static int IndexOf<T>(this IEnumerable<T> items, T value, IEqualityComparer<T> comparer = null)
        {
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            var index = 0;

            foreach (var item in items)
            {
                if (comparer.Equals(value, item))
                    return index;

                index++;
            }

            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var index = 0;

            foreach (var item in items)
            {
                if (predicate(item))
                    return index;

                index++;
            }

            return -1;
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> items, params T[] args)
        {
            return args.Concat(items);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> items, IEnumerable<T> second)
        {
            return second.Concat(items);
        }

        public static IEnumerable<T> TrimNull<T>(this IEnumerable<T> items)
        {
            return items.Where(item => item != null);
        }
    }
}
