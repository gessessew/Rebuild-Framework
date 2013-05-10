using System;
using System.Collections.Generic;
using System.Threading;

namespace Rebuild.Utils
{
    public static partial class Memoizer
    {
        public static Func<TArg, TResult> LocalThreadMemoizeOne<TArg, TResult>(Func<TArg, TResult> func, IEqualityComparer<TArg> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TArg>.Default;
            }

            var local = new ThreadLocal<KeyValuePair<TArg, TResult>>();

            return arg =>
            {
                KeyValuePair<TArg, TResult> kv;

                if (!local.IsValueCreated || !comparer.Equals((kv = local.Value).Key, arg))
                {
                    local.Value = kv = new KeyValuePair<TArg, TResult>(arg, func(arg));
                }

                return kv.Value;
            };
        }

        public static Func<TArg, TResult> LocalThreadMemoizeOne<TArg, TResult>(Func<TArg, TResult> func, TimeSpan expiration, IEqualityComparer<TArg> comparer = null)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TArg>.Default;
            }

            var local = new ThreadLocal<Record<TArg, TResult>>();

            return arg =>
            {
                Record<TArg, TResult> record;

                if (!local.IsValueCreated || !comparer.Equals((record = local.Value).Arg, arg) || (record.Time + expiration <= DateTime.UtcNow))
                {
                    local.Value = record = new Record<TArg, TResult>
                    {
                        Arg = arg,
                        Result = func(arg),
                        Time = DateTime.UtcNow
                    };
                }

                return record.Result;
            };
        }

        public static Func<TArg, TResult> Memoize<TArg, TResult>(Func<TArg, TResult> func, Func<MemoizeBuilder, MemoizeBuilder> builder)
        {
            return null;
        }

        public static void Test()
        {
            var func = Memoizer.Memoize<int, int>(a => a, b => b
                .Capacity(1)
                .Expiration(TimeSpan.FromSeconds(1))
                .LocalThread()
            );

            Memoizer.LocalThreadMemoizeOne<int, int>(a => a, TimeSpan.FromSeconds(1));
        }

        #region struct Record<TArg, TResult>
        private struct Record<TArg, TResult>
        {
            internal TArg Arg;
            internal TResult Result;
            internal DateTime Time;
        }
        #endregion
    }

    public class MemoizeBuilder
    {
        public MemoizeBuilder Capacity(int value) { return null; }
        public MemoizeBuilder LocalThread() { return null; }
        public MemoizeBuilder Expiration(TimeSpan ts) { return null; }
    }
}
