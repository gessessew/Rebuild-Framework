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

        /// <summary>
        /// Returns the result of the selector Func if the value is not null, otherwise returns the default value.
        /// </summary>
        public static TValue IfNotNull<T, TValue>(this T value, Func<T, TValue> selector, TValue defaultValue = default(TValue))
        {
            return value == null ? defaultValue : selector(value);
        }

        /// <summary>
        /// Returns the result of the selector Func if the value is not null, otherwise returns the result of the selectorIfNull Func.
        /// </summary>
        public static TValue IfNotNull<T, TValue>(this T value, Func<T, TValue> selector, Func<TValue> selectorIfNull)
        {
            return value == null ? selectorIfNull() : selector(value);
        }

        /// <summary>
        /// Returns the result of the provider Func if the value is null, otherwise returns the value.
        /// </summary>
        public static T IfNull<T>(this T value, Func<T> provider)
        {
            return value == null ? provider() : value;
        }

        /// <summary>
        /// Returns the a default value if the value is null, otherwise returns the value.
        /// </summary>
        public static T IfNull<T>(this T value, T defaultValue = default(T))
        {
            return value == null ? defaultValue : value;
        }

        /// <summary>
        /// Invokes the action passing the value as parameter and returns the value.
        /// </summary>
        public static T With<T>(this T value, Action<T> action)
        {
            action(value);
            return value;
        }

        /// <summary>
        /// Invokes the action if the value is not null returns the value.
        /// </summary>
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
