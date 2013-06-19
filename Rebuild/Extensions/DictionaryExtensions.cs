using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static partial class DictionaryExtensions
    {
        public static AddIfNotExistsResult<TKey, TValue> AddIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> dic, KeyValuePair<TKey, TValue> keyValuePair)
        {
            var isAdded = !dic.ContainsKey(keyValuePair.Key);

            if (isAdded)
            {
                dic.Add(keyValuePair);
            }

            return new AddIfNotExistsResult<TKey, TValue>(keyValuePair.Key, keyValuePair.Value, dic, isAdded);
        }

        public static AddIfNotExistsResult<TKey, TValue> AddIfNotExists<TKey, TValue>(this  IDictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            var isAdded = !dic.ContainsKey(key);

            if (isAdded)
            {
                dic.Add(key, value);
            }

            return new AddIfNotExistsResult<TKey, TValue>(key, value, dic, isAdded);
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            return dic == null || !dic.TryGetValue(key, out value) ? defaultValue : value;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TValue> selector)
        {
            TValue value;
            return dic == null || !dic.TryGetValue(key, out value) ? selector() : value;
        }

        public static Func<TKey, TValue> Memoize<TKey, TValue>(this IDictionary<TKey, TValue> dic, Func<TKey, TValue> factory)
        {
            return arg =>
            {
                TValue value;
                return dic.TryGetValue(arg, out value) ? value : dic[arg] = factory(arg);
            };
        }

        public static IDictionary<TKey, TValue> RemoveKeys<TKey, TValue>(this IDictionary<TKey, TValue> dic, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                dic.Remove(key);
            }

            return dic;
        }
    }
}
