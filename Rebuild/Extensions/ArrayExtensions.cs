using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Clone<T>(this T[] items)
        {
            return (T[])items.Clone();
        }

        public static T[] Sort<T>(this T[] items, IComparer<T> comparer = null)
        {
            Array.Sort(items, comparer);
            return items;
        }
    }
}
