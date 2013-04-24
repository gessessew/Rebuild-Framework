using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class DictionaryExtensions
    {
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
    }
}
