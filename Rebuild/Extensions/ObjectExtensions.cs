using System;

namespace Rebuild.Extensions
{
    public static class ObjectExtensions
    {
        public static TValue SelectOrDefault<T, TValue>(this T obj, Func<T, TValue> selector, TValue defaultValue = default(TValue))
        {
            return obj == null ? defaultValue : selector(obj);
        }

        public static TValue SelectOrDefault<T, TValue>(this T obj, Func<T, TValue> selector, Func<TValue> selectorIfNull)
        {
            return obj == null ? selectorIfNull() : selector(obj);
        }

        public static T With<T>(this T value, Action<T> action)
        {
            action(value);
            return value;
        }

        public static T WithIfNotNull<T>(this T value, Action<T> action)
        {
            if (value != null)
            {
                action(value);
            }

            return value;
        }
    }
}
