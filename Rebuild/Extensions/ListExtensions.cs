using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class ListExtensions
    {
        public static BinaryInsertResult<T> BinaryInsert<T>(this IList<T> list, T value, int index = 0, int length = int.MaxValue, IComparer<T> comparer = null, BinaryInsertStrategy insertStrategy = BinaryInsertStrategy.DoNothingIfFound)
        {
            return BinaryInsert(list, value, k => k, index, length, comparer, insertStrategy);
        }

        public static BinaryInsertResult<T> BinaryInsert<T, TKey>(this IList<T> list, T item, Func<T, TKey> keySelector, int index = 0, int length = int.MaxValue, IComparer<TKey> comparer = null, BinaryInsertStrategy insertStrategy = BinaryInsertStrategy.DoNothingIfFound)
        {
            index = BinarySearch(list, keySelector(item), keySelector, index, length, comparer);

            if (index > -1)
            {
                switch (insertStrategy)
                {
                    case BinaryInsertStrategy.DoNothingIfFound:
                        return new BinaryInsertResult<T>(list, item, index, false);

                    case BinaryInsertStrategy.InsertAfter:
                        index++;
                        break;

                    case BinaryInsertStrategy.Replace:
                        list[index] = item;
                        return new BinaryInsertResult<T>(list, item, index, true);
                }
            }
            else
            {
                index = ~index;
            }

            list.Insert(index, item);
            return new BinaryInsertResult<T>(list, item, index, true);
        }

        public static BinaryRemoveResult<T> BinaryRemove<T>(this IList<T> list, T value, int index = 0, int length = int.MaxValue, IComparer<T> comparer = null)
        {
            return BinaryRemove(list, value, k => k, index, length, comparer);
        }

        public static BinaryRemoveResult<T> BinaryRemove<T, TKey>(this IList<T> list, TKey value, Func<T, TKey> keySelector, int index = 0, int length = int.MaxValue, IComparer<TKey> comparer = null)
        {
            index = BinarySearch(list, value, keySelector, index, length, comparer);
            var removedItem = default(T);

            if (index > -1)
            {
                removedItem = list[index];
                list.RemoveAt(index);
            }

            return new BinaryRemoveResult<T>(list, removedItem, index);
        }

        public static int BinarySearch<T>(this IList<T> list, T value, int index = 0, int length = int.MaxValue, IComparer<T> comparer = null)
        {
            return BinarySearch<T, T>(list, value, key => key, index, length, comparer);
        }

        public static int BinarySearch<T, TKey>(this IList<T> list, TKey value, Func<T, TKey> keySelector, int index = 0, int length = int.MaxValue, IComparer<TKey> comparer = null)
        {
            if (comparer == null)
                comparer = Comparer<TKey>.Default;

            length = Math.Min(list.Count, index + length - 1);

            while (index <= length)
            {
                int i = index + (length - index >> 1);

                var j = list[i] == null
                    ? value == null ? 0 : -1
                    : comparer.Compare(keySelector(list[i]), value);

                if (j == 0)
                    return i;

                if (j < 0)
                    index = i + 1;
                else
                    length = i - 1;
            }

            return ~index;
        }

        public static IList<T> Swap<T>(this IList<T> list, int index1, int index2)
        {
            var item = list[index1];
            list[index1] = list[index2];
            list[index2] = item;
            return list;
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
