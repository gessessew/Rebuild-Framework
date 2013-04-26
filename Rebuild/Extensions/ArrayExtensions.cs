using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Creates a shallow copy of the <typeparamref name="System.Array"/>.
        /// </summary>
        public static T[] Clone<T>(this T[] items)
        {
            return (T[])items.Clone();
        }

        /// <summary>
        /// Sort the elements of an array using the specified comparer.
        /// </summary>
        public static T[] Sort<T>(this T[] items, IComparer<T> comparer = null)
        {
            Array.Sort(items, comparer);
            return items;
        }
    }
}
