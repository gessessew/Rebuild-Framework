using System;
using System.Collections.Generic;

namespace Rebuild.Utils
{
    public static class Memoizer
    {
        public static Func<TArg, TResult> Memoize<TArg, TResult>(Func<TArg, TResult> factory, IEqualityComparer<TArg> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TArg>.Default;
            }

            var dic = new Dictionary<TArg,TResult>(comparer);
            var defaultResult = default(TResult);
            var hasDefaultResult = false;

            return arg =>
            {
                if (comparer.Equals(arg, default(TArg)))
                {
                    if (!hasDefaultResult)
                    {
                        defaultResult = factory(arg);
                        hasDefaultResult = true;
                    }

                    return defaultResult;
                }

                TResult result;

                if (!dic.TryGetValue(arg, out result))
                {
                    dic[arg] = result = factory(arg);
                }

                return result;
            };
        }

        public static Func<TArg, TResult> MemoizeOne<TArg, TResult>(Func<TArg, TResult> factory, IEqualityComparer<TArg> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TArg>.Default;
            }

            var argValue = default(TArg);
            var hasValue = false;
            var resultValue = default(TResult);

            return arg =>
            {
                if (!hasValue || !comparer.Equals(argValue, arg))
                {
                    resultValue = factory(arg);
                    hasValue = true;
                    argValue = arg;
                }

                return resultValue;
            };
        }
    }
}
