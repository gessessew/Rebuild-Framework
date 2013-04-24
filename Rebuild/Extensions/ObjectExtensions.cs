using System;

namespace Rebuild.Extensions
{
    public static class ObjectExtensions
    {
        public static T Do<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        public static TValue SelectOrDefault<T, TValue>(this T obj, Func<T, TValue> selector, TValue defaultValue = default(TValue))
        {
            return obj == null || selector == null ? defaultValue : selector(obj);
        }
    }
}
