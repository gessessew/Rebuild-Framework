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

        public static T DoIfNotNull<T>(this T obj, Action<T> action)
        {
            if (obj != null)
                action(obj);

            return obj;
        }

        public static TValue IfNotNull<T, TValue>(this T obj, Func<T, TValue> selector, TValue defaultValue = default(TValue))
        {
            return obj == null ? defaultValue : selector(obj);
        }

        public static TValue IfNotNull<T, TValue>(this T obj, Func<T, TValue> selector, Func<TValue> selectorIfNull)
        {
            return obj == null ? selectorIfNull() : selector(obj);
        }
    }
}
