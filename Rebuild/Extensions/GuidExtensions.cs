using System;

namespace Rebuild.Extensions
{
    public static class GuidExtensions
    {
        public static bool HasValue(this Guid guid)
        {
            return guid != Guid.Empty;
        }

        public static bool HasValue(this Guid? guid)
        {
            return guid != null && guid.Value != Guid.Empty;
        }

        public static TValue IfHasValue<TValue>(this Guid guid, Func<Guid, TValue> selector, TValue defaultValue = default(TValue))
        {
            return guid == Guid.Empty ? defaultValue : selector(guid);
        }

        public static TValue IfHasValue<TValue>(this Guid guid, Func<Guid, TValue> selector, Func<TValue> provider)
        {
            return guid == Guid.Empty ? provider() : selector(guid);
        }

        public static TValue IfHasValue<TValue>(this Guid? guid, Func<Guid, TValue> selector, TValue defaultValue = default(TValue))
        {
            return guid.IsNullOrEmpty() ? defaultValue : selector(guid.Value);
        }

        public static TValue IfHasValue<TValue>(this Guid? guid, Func<Guid, TValue> selector, Func<TValue> provider)
        {
            return guid.IsNullOrEmpty() ? provider() : selector(guid.Value);
        }

        public static Guid IfNoValue(this Guid guid, Func<Guid> provider)
        {
            return guid == Guid.Empty ? provider() : guid;
        }

        public static Guid IfNoValue(this Guid? guid, Func<Guid> provider)
        {
            return guid.IsNullOrEmpty() ? provider() : guid.Value;
        }

        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return guid == null || guid.Value == Guid.Empty;
        }
    }
}
