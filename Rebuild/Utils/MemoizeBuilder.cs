using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Rebuild.Utils
{
    public partial class MemoizeBuilder<TArg, TResult>
    {
        private int _capacity;
        private IEqualityComparer<TArg> _comparer;
        private Func<TArg, TResult, DateTime, bool> _expiration;
        private bool _threadSafe;

        internal MemoizeBuilder()
        {
            _capacity = int.MaxValue;
        }

        internal Func<TArg, TResult> Build(Func<TArg, TResult> factory)
        {
            var threadSafe = _threadSafe;
#if NETFX
            if (_threadLocal && threadSafe)
            {
                factory = SynchronizeFunc(factory);
            }
#endif
            var comparer = _comparer ?? EqualityComparer<TArg>.Default;
            Func<TArg, TResult> result;

            if (_capacity == 1)
            {
                var provider = CreateSingleValue();
                result = Memoizer.MemoizeOneInternal(factory, _expiration, comparer, provider);
            }
            else
            {
                var provider = CreateMultiValue(comparer);
                result = Memoizer.MemoizeInternal(factory, _expiration, _capacity, provider);
            }

            if (threadSafe)
            {
                result = SynchronizeFunc(factory);
            }

            return result;
        }

        private Func<Memoizer.SingleContainer<TArg, TResult>> CreateSingleValue()
        {
#if NETFX
            if (_threadLocal)
            {
                return CreateSingleValueThreadLocal();
            }
#endif
            var container = new Memoizer.SingleContainer<TArg, TResult>();
            return () => container;
        }

        private Func<Memoizer.MultiContainer<TArg, TResult>> CreateMultiValue(IEqualityComparer<TArg> comparer)
        {
#if NETFX
            if (_threadLocal)
            {
                return CreateMultiValueThreadLocal(comparer);
            }
#endif
            var container = new Memoizer.MultiContainer<TArg, TResult>(comparer);
            return () => container;
        }

        public MemoizeBuilder<TArg, TResult> Capacity(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentException("capacity is invalid.");
            }

            _capacity = capacity;
            return this;
        }

        public MemoizeBuilder<TArg, TResult> Comparer(IEqualityComparer<TArg> comparer)
        {
            _comparer = comparer;
            return this;
        }

        public MemoizeBuilder<TArg, TResult> Expiration(TimeSpan expiration)
        {
            _expiration = (arg, result, time) => time.Add(expiration) <= DateTime.UtcNow;
            return this;
        }

        public MemoizeBuilder<TArg, TResult> Expiration(Func<TArg, TResult, DateTime, bool> expiration)
        {
            _expiration = expiration;
            return this;
        }

        private static Func<TArg, TResult> SynchronizeFunc(Func<TArg, TResult> func)
        {
            var gate = new object();

            return arg =>
            {
                lock (gate)
                {
                    return func(arg);
                }
            };
        }

        public MemoizeBuilder<TArg, TResult> ThreadSafe()
        {
            _threadSafe = true;
            return this;
        }
    }
}
