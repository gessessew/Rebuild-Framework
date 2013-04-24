using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class ListExtensions
    {
        public static IList<T> BinaryInsert<T>(this IList<T> items, T value, int index = 0, int length = int.MaxValue, IComparer<T> comparer = null, BinaryInsertStrategy insertStrategy = BinaryInsertStrategy.DoNothingIfFound)
        {
            return BinaryInsert(items, value, k => k, index, length, comparer, insertStrategy);
        }

        public static IList<T> BinaryInsert<T, TKey>(this IList<T> items, T value, Func<T, TKey> keySelector, int index = 0, int length = int.MaxValue, IComparer<TKey> comparer = null, BinaryInsertStrategy insertStrategy = BinaryInsertStrategy.DoNothingIfFound)
        {
            index = BinarySearch(items, keySelector(value), keySelector, index, length, comparer);
            if (index > -1)
            {
                switch (insertStrategy)
                {
                    case BinaryInsertStrategy.DoNothingIfFound:
                        return items;

                    case BinaryInsertStrategy.InsertAfter:
                        index++;
                        break;

                    case BinaryInsertStrategy.Replace:
                        items[index] = value;
                        return items;
                }
            }
            else
            {
                index = ~index;
            }

            items.Insert(index, value);
            return items;
        }

        public static bool BinaryRemove<T>(this IList<T> items, T value, int index = 0, int length = int.MaxValue, IComparer<T> comparer = null)
        {
            return BinaryRemove(items, value, k => k, index, length, comparer);
        }

        public static bool BinaryRemove<T, TKey>(this IList<T> items, TKey value, Func<T, TKey> keySelector, int index = 0, int length = int.MaxValue, IComparer<TKey> comparer = null)
        {
            index = BinarySearch(items, value, keySelector, index, length, comparer);
            if (index > -1)
                items.RemoveAt(index);

            return index > -1;
        }

        public static int BinarySearch<T>(this IList<T> items, T value, int index = 0, int length = int.MaxValue, IComparer<T> comparer = null)
        {
            return BinarySearch<T, T>(items, value, key => key, index, length, comparer);
        }

        public static int BinarySearch<T, TKey>(this IList<T> items, TKey value, Func<T, TKey> keySelector, int index = 0, int length = int.MaxValue, IComparer<TKey> comparer = null)
        {
            if (comparer == null)
                comparer = Comparer<TKey>.Default;

            length = Math.Min(items.Count, index + length - 1);

            while (index <= length)
            {
                int i = index + (length - index >> 1);

                var j = items[i] == null
                    ? value == null ? 0 : -1
                    : comparer.Compare(keySelector(items[i]), value);

                if (j == 0)
                    return i;

                if (j < 0)
                    index = i + 1;
                else
                    length = i - 1;
            }

            return ~index;
        }

        public static IList<T> Swap<T>(this IList<T> items, int index1, int index2)
        {
            var item = items[index1];
            items[index1] = items[index2];
            items[index2] = item;
            return items;
        }
    }

    #region enum BinaryInsertStrategy
    public enum BinaryInsertStrategy
    {
        InsertAfter,
        InsertBefore,
        Replace,
        DoNothingIfFound
    }
    #endregion
}
