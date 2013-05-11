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
            return AddRange(collection, itemsToAdd.EmptyIfNull().ToArray());
        }

        public static AddRangeResult<T> AddRange<T>(this ICollection<T> collection, params T[] itemsToAdd)
        {
            if (itemsToAdd == null)
            {
                itemsToAdd = new T[0];
            }

            for (var i = 0; i < itemsToAdd.Length; i++)
            {
                collection.Add(itemsToAdd[i]);
            }

            return new AddRangeResult<T>(collection, itemsToAdd);
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

        public static AddRangeResult<T> AddRangeIfNotExists<T>(this ICollection<T> collection, IEqualityComparer<T> comparer = null, params T[] itemsToAdd)
        {
            return AddRangeIfNotExists(collection, (IEnumerable<T>)itemsToAdd, comparer);
        }

        public static RemoveRangeResult<T> RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToRemove)
        {
            return RemoveRange(collection, itemsToRemove.EmptyIfNull().ToArray());
        }

        public static RemoveRangeResult<T> RemoveRange<T>(this ICollection<T> collection, params T[] itemsToRemove)
        {
            if (itemsToRemove == null)
            {
                itemsToRemove = new T[0];
            }

            for (var i = 0; i < itemsToRemove.Length; i++)
            {
                collection.Remove(itemsToRemove[i]);
            }

            return new RemoveRangeResult<T>(collection, itemsToRemove);
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
