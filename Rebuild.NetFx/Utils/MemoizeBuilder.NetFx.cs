using System;
using System.Collections.Generic;
using System.Threading;

namespace Rebuild.Utils
{
    partial class MemoizeBuilder<TArg, TResult>
    {
        private bool _threadLocal;

        private Func<Memoizer.MultiContainer<TArg, TResult>> CreateMultiValueThreadLocal(IEqualityComparer<TArg> comparer)
        {
            return () =>
            {
                var l = new ThreadLocal<Memoizer.MultiContainer<TArg, TResult>>();

                if (!l.IsValueCreated)
                {
                    l.Value = new Memoizer.MultiContainer<TArg, TResult>(comparer);
                }

                return l.Value;
            };
        }

        private static Func<Memoizer.SingleContainer<TArg, TResult>> CreateSingleValueThreadLocal()
        {
            return () =>
            {
                var l = new ThreadLocal<Memoizer.SingleContainer<TArg, TResult>>();

                if (!l.IsValueCreated)
                {
                    l.Value = new Memoizer.SingleContainer<TArg, TResult>();
                }

                return l.Value;
            };
        }

        public MemoizeBuilder<TArg, TResult> ThreadLocal()
        {
            _threadLocal = true;
            return this;
        }
    }
}
