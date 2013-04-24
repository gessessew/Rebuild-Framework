using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddIfNotExists<T>(this ICollection<T> collection, T item)
        {
            if (!collection.Contains(item))
                collection.Add(item);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            if (itemsToAdd != null)
            {
                foreach (var item in itemsToAdd)
                {
                    collection.Add(item);
                }
            }
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToRemove)
        {
            if (itemsToRemove != null)
            {
                foreach (var item in itemsToRemove)
                {
                    collection.Remove(item);
                }
            }
        }
    }
}
