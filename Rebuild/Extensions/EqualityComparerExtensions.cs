using System.Collections;
using System.Collections.Generic;

namespace Rebuild.Extensions
{
    public static class EqualityComparerExtensions
    {
        public static IEqualityComparer ToUnTypedEqualityComparer<T>(this IEqualityComparer<T> comparer)
        {
            return new UnTypedEqualityComparer<T>(comparer);
        }

        #region class UnTypedEqualityComparer<T>
        private sealed class UnTypedEqualityComparer<T> : IEqualityComparer
        {
            private readonly IEqualityComparer<T> _comparer;

            public UnTypedEqualityComparer(IEqualityComparer<T> comparer)
            {
                _comparer = comparer;
            }

            bool IEqualityComparer.Equals(object x, object y)
            {
                return _comparer.Equals((T)x, (T)y);
            }

            int IEqualityComparer.GetHashCode(object obj)
            {
                return _comparer.GetHashCode((T)obj);
            }
        }
        #endregion
    }
}
