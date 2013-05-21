using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static partial class EnumerableExtensions
    {
        public static AddToResult<T, TCollection> AddTo<T, TCollection>(this IEnumerable<T> items, TCollection collection) where TCollection : ICollection<T>
        {
            var array = items.ToArray();

            if (array.Length != 0)
            {
                collection.AddRange(array);
            }

            return new AddToResult<T, TCollection>(array, collection);
        }

        public static IEnumerable<T[]> Batch<T>(this IEnumerable<T> items, int batchSize)
        {
            var list = new List<T>(batchSize);

            foreach (var item in items)
            {
                list.Add(item);
                if (list.Count == batchSize)
                {
                    var array = list.ToArray();
                    list.Clear();
                    yield return array;
                }
            }

            if (list.Count != 0)
                yield return list.ToArray();
        }

        public static IEnumerable<T[]> Batch<T>(this IEnumerable<T> items, Func<T, IList<T>, bool> splitPredicate)
        {
            var list = new List<T>();

            foreach (var item in items)
            {
                list.Add(item);

                if (splitPredicate(item, list))
                {
                    var array = list.ToArray();
                    list.Clear();
                    yield return array;
                }
            }

            if (list.Count != 0)
                yield return list.ToArray();
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, params T[] args)
        {
            return args == null ? items : items.Concat((IEnumerable<T>)args);
        }

        public static CopyToResult<T> CopyTo<T>(this IEnumerable<T> source, T[] destination, int destinationStartIndex)
        {
            var array = source.Take(Math.Max(destination.Length - destinationStartIndex, 0)).ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                destination[i + destinationStartIndex] = array[i];
            }

            return new CopyToResult<T>(source, destination, array);
        }

        public static bool ContainsAll<T>(this IEnumerable<T> items, params T[] searchedItems)
        {
            return ContainsAll(items, searchedItems, default(IEqualityComparer<T>));
        }

        public static bool ContainsAll<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer, params T[] searchedItems)
        {
            return ContainsAll(items, searchedItems, comparer);
        }

        public static bool ContainsAll<T>(this IEnumerable<T> items, IEnumerable<T> searchedItems, IEqualityComparer<T> comparer = null)
        {
            if (searchedItems == null)
            {
                return true;
            }

            var dic = searchedItems.ToDictionary(k => k, v => true, comparer);

            if (dic.Count == 0)
            {
                return true;
            }

            foreach(var item in items)
            {
                if (dic.Remove(item) && dic.Count == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> items, params T[] searchedItems)
        {
            return items.IndexOfAny(searchedItems, default(IEqualityComparer<T>)) != -1;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer, params T[] searchedItems)
        {
            return items.IndexOfAny(searchedItems, comparer) != -1;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> items, IEnumerable<T> searchedItems, IEqualityComparer<T> comparer = null)
        {
            return items.IndexOfAny(searchedItems, comparer) != -1;
        }

        public static bool ContainsType<TSearchType>(this IEnumerable items, TSearchType typeInferance = default(TSearchType))
        {
            foreach (var item in items)
            {
                if (item is TSearchType)
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> DoIfNotNull<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                if (item != null)
                {
                    action(item);
                }

                yield return item;
            }
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            var dic = new Dictionary<TKey, bool>(comparer);
            
            foreach(var item in items)
            {
                if (dic.AddIfNotExists(keySelector(item), true).IsAdded)
                {
                    yield return item;
                }
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

        public static void ForEach<T>(this IEnumerable<T> items, Action<T,int> action)
        {
            var i = 0;

            foreach (var item in items)
            {
                action(item, i++);
            }
        }

        public static void ForEachNotNull<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                if (item != null)
                {
                    action(item);
                }
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

        public static int IndexOfAny<T>(this IEnumerable<T> items, params T[] searchedItems)
        {
            return items.IndexOfAny(searchedItems, default(IEqualityComparer<T>));
        }

        public static int IndexOfAny<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer, params T[] searchedItems)
        {
            return items.IndexOfAny(searchedItems, comparer);
        }

        public static int IndexOfAny<T>(this IEnumerable<T> items, IEnumerable<T> searchedItems, IEqualityComparer<T> comparer = null)
        {
            if (searchedItems == null)
                return -1;

            var dic = new Dictionary<T, bool>(comparer);
            foreach (var a in searchedItems)
            {
                dic[a] = true;
            }

            if (dic.Count == 0)
                return -1;

            var index = 0;

            foreach (var item in items)
            {
                if (dic.ContainsKey(item))
                    return index;

                index++;
            }

            return -1;
        }

        public static int IndexOfType<TSearchType>(this IEnumerable items, TSearchType typeInferance = default(TSearchType))
        {
            var index = 0;
            foreach (var item in items)
            {
                if (item is TSearchType)
                    return index;

                index++;
            }

            return -1;
        }

        public static int LastIndexOf<T>(this IEnumerable<T> items, T value, IEqualityComparer<T> comparer = null)
        {
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            var index = -1;
            var i = 0;

            foreach (var item in items)
            {
                if (comparer.Equals(value, item))
                    index = i;

                i++;
            }

            return index;
        }

        public static int LastIndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var index = -1;
            var i = 0;

            foreach (var item in items)
            {
                if (predicate(item))
                    index = i;

                i++;
            }

            return index;
        }

        public static int LastIndexOfAny<T>(this IEnumerable<T> items, params T[] searchedItems)
        {
            return items.LastIndexOfAny(searchedItems, default(IEqualityComparer<T>));
        }

        public static int LastIndexOfAny<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer, params T[] searchedItems)
        {
            return items.LastIndexOfAny(searchedItems, comparer);
        }

        public static int LastIndexOfAny<T>(this IEnumerable<T> items, IEnumerable<T> searchedItems, IEqualityComparer<T> comparer = null)
        {
            var dic = new Dictionary<T, bool>(comparer);
            foreach (var a in searchedItems)
            {
                dic[a] = true;
            }

            if (dic.Count == 0)
                return -1;

            var i = 0;
            var index = -1;

            foreach (var item in items)
            {
                if (dic.ContainsKey(item))
                    index = i;

                i++;
            }

            return index;
        }

        public static int LastIndexOfType<TSearchType>(this IEnumerable items, TSearchType typeInferance = default(TSearchType))
        {
            var index = -1;
            var i = 0;

            foreach (var item in items)
            {
                if (item is TSearchType)
                    index = i;

                i++;
            }

            return index;
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> items, params T[] args)
        {
            return args == null || args.Length == 0 ? items : args.Concat(items);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> items, IEnumerable<T> second)
        {
            return second == null ? items : second.Concat(items);
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> items, Random random = null)
        {
            if (random == null)
            {
                random = new Random();
            }

            return items.OrderBy(x => random.Next());
        }

        public static RemoveFromResult<T, TCollection> RemoveFrom<T, TCollection>(this IEnumerable<T> items, TCollection collection) where TCollection : ICollection<T>
        {
            return new RemoveFromResult<T, TCollection>(collection.RemoveRange(items).RemovedItems, collection);
        }

        public static TDestination RemoveKeysFrom<TKey, TValue, TDestination>(this IEnumerable<TKey> items, TDestination destination) where TDestination : IDictionary<TKey, TValue>
        {
            destination.RemoveKeys(items);
            return destination;
        }

        public static bool SequenceEqual<T>(this IEnumerable<T> first, params T[] second)
        {
            return Enumerable.SequenceEqual<T>(first, second);
        }

        public static bool SequenceEqual<T>(this IEnumerable<T> first, IEqualityComparer<T> comparer = null, params T[] second)
        {
            return Enumerable.SequenceEqual<T>(first, second, comparer);
        }

        public static Dictionary<TKey, T> ToDictionary<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector, DictionaryDistinctMode distinctMode, IEqualityComparer<TKey> comparer = null)
        {
            var dic = new Dictionary<TKey, T>(comparer);

            foreach (var item in items)
            {
                var key = keySelector(item);

                if (distinctMode == DictionaryDistinctMode.LastFound || !dic.ContainsKey(key))
                {
                    dic[key] = item;
                }
            }

            return dic;
        }

        public static Dictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(this IEnumerable<T> items, Func<T, TKey> keySelector, Func<T, TValue> valueSelector, DictionaryDistinctMode distinctMode = DictionaryDistinctMode.LastFound, IEqualityComparer<TKey> comparer = null)
        {
            var dic = new Dictionary<TKey, TValue>(comparer);

            foreach (var item in items)
            {
                var key = keySelector(item);

                if (distinctMode == DictionaryDistinctMode.LastFound || !dic.ContainsKey(key))
                {
                    dic[key] = valueSelector(item);
                }
            }

            return dic;
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> items)
        {
            return new Queue<T>(items);
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> items)
        {
            return new Stack<T>(items);
        }

        public static IEnumerable<T> TrimNull<T>(this IEnumerable<T> items)
        {
            return items.Where(item => item != null);
        }
    }

    #region enum DictionaryDistinctMode
    public enum DictionaryDistinctMode
    {
        FirstFound,
        LastFound
    }
    #endregion
}
