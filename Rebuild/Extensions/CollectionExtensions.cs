using System;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static class CollectionExtensions
    {
        public static ICollection<T> AddIfNotExists<T>(this ICollection<T> collection, T item, IEqualityComparer<T> comparer = null)
        {
            if (!collection.Contains(item, comparer))
                collection.Add(item);

            return collection;
        }

        public static ICollection<T> AddIfNotExists<T>(this ICollection<T> collection, Func<T, bool> predicate, T item)
        {
            if (!collection.Any(predicate))
                collection.Add(item);

            return collection;
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            if (itemsToAdd != null)
            {
                foreach (var item in itemsToAdd)
                {
                    collection.Add(item);
                }
            }

            return collection;
        }

        public static ICollection<T> RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToRemove)
        {
            if (itemsToRemove != null)
            {
                foreach (var item in itemsToRemove)
                {
                    collection.Remove(item);
                }
            }

            return collection;
        }
    }
}
