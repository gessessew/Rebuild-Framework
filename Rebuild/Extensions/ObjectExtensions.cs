using System;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static partial class ObjectExtensions
    {
        public static bool EqualsAny<T>(this T obj, IEnumerable<T> values, IEqualityComparer<T> comparer = null)
        {
            if (values == null)
            {
                return false;
            }

            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            foreach (var v in values)
            {
                if (comparer.Equals(v, obj))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EqualsAny<T>(this T obj, IEqualityComparer<T> comparer = null, params T[] args)
        {
            if (args == null || args.Length == 0)
            {
                return false;
            }

            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            foreach (var a in args)
            {
                if (comparer.Equals(a, obj))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EqualsAny<T>(this T obj, T arg0, T arg1, IEqualityComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            return comparer.Equals(obj, arg0) || comparer.Equals(obj, arg1);
        }

        public static bool EqualsAny<T>(this T obj, T arg0, T arg1, T arg2, IEqualityComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            return comparer.Equals(obj, arg0) || comparer.Equals(obj, arg1) || comparer.Equals(obj, arg2);
        }

        public static bool EqualsAny<T>(this T obj, T arg0, T arg1, T arg2, T arg3, IEqualityComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            return comparer.Equals(obj, arg0) || comparer.Equals(obj, arg1) || comparer.Equals(obj, arg2) || comparer.Equals(obj, arg3);
        }

        public static bool EqualsAny(this string s, IEqualityComparer<string> comparer = null, params string[] args)
        {
            return s.EqualsAny((IEnumerable<string>)args, comparer);
        }

        public static bool EqualsAny(this string s, string s0, string s1, IEqualityComparer<string> comparer = null)
        {
            if (comparer == null)
            {
                comparer = StringComparer.Ordinal;
            }

            return comparer.Equals(s, s0) || comparer.Equals(s, s1);
        }

        public static TValue IfNotNull<T, TValue>(this T obj, Func<T, TValue> selector, TValue defaultValue = default(TValue))
        {
            return obj == null ? defaultValue : selector(obj);
        }

        public static TValue IfNotNull<T, TValue>(this T obj, Func<T, TValue> selector, Func<TValue> selectorIfNull)
        {
            return obj == null ? selectorIfNull() : selector(obj);
        }

        public static T IfNull<T>(this T obj, Func<T> provider)
        {
            return obj == null ? provider() : obj;
        }

        public static T IfNull<T>(this T obj, T defaultValue = default(T))
        {
            return obj == null ? defaultValue : obj;
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
