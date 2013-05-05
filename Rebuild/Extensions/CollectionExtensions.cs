using System;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static class CollectionExtensions
    {
        public static AddIfNotExistsResult<T> AddIfNotExists<T>(this ICollection<T> collection, T item, IEqualityComparer<T> comparer = null)
        {
            var isAdded = !collection.Contains(item, comparer);

            if (isAdded)
            {
                collection.Add(item);
            }

            return new AddIfNotExistsResult<T>(item, collection, isAdded);
        }

        public static AddRangeResult<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            var array = itemsToAdd.EmptyIfNull().ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                collection.Add(array[i]);
            }

            return new AddRangeResult<T>(collection, array);
        }

        public static AddRangeResult<T> AddRangeIfNotExists<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd, IEqualityComparer<T> comparer = null)
        {
            var array = itemsToAdd.Except(collection, comparer).ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                collection.Add(array[i]);
            }

            return new AddRangeResult<T>(collection, array);
        }

        public static RemoveRangeResult<T> RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToRemove)
        {
            var array = itemsToRemove.EmptyIfNull().ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                collection.Remove(array[i]);
            }

            return new RemoveRangeResult<T>(collection, array);
        }

        public static RemoveRangeResult<T> RemoveRange<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            var array = collection.Where(predicate).ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                collection.Remove(array[i]);
            }

            return new RemoveRangeResult<T>(collection, array);
        }

        public static RemoveRangeResult<T> RemoveRange<T>(this ICollection<T> collection, int startIndex, int count = int.MaxValue)
        {
            var array = collection.Skip(startIndex).Take(count).ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                collection.Remove(array[i]);
            }

            return new RemoveRangeResult<T>(collection, array);
        }
    }
}
