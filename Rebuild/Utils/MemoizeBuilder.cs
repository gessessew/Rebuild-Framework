using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Rebuild.Utils
{
    public partial class MemoizeBuilder<TArg>
    {
        private int _capacity;
        private IEqualityComparer<TArg> _comparer;
        private TimeSpan _expiration;
        private bool _threadSafe;

        public MemoizeBuilder()
        {
            _capacity = int.MaxValue;
            _expiration = TimeSpan.MaxValue;
        }

        internal Func<TArg, TResult> Build<TResult>(Func<TArg, TResult> factory)
        {
            var comparer = _comparer ?? EqualityComparer<TArg>.Default;
            Func<TArg, TResult> result;

            if (_capacity == 1)
            {
                var provider = CreateSingleValue<TResult>();
                result = Memoizer.MemoizeOneInternal(factory, _expiration, comparer, provider);
            }
            else
            {
                var provider = CreateMultiValue<TResult>(comparer);
                result = Memoizer.MemoizeInternal(factory, _expiration, _capacity, provider);
            }

            return result;
        }

        private Func<Memoizer.SingleContainer<TArg, TResult>> CreateSingleValue<TResult>()
        {
#if NETFX
            if (_threadLocal)
            {
                return CreateSingleValueThreadLocal<TResult>();
            }
#endif
            var container = new Memoizer.SingleContainer<TArg, TResult>();
            return () => container;
        }

        private Func<Memoizer.MultiContainer<TArg, TResult>> CreateMultiValue<TResult>(IEqualityComparer<TArg> comparer)
        {
#if NETFX
            if (_threadLocal)
            {
                return CreateMultiValueThreadLocal<TResult>(comparer);
            }
#endif
            var container = new Memoizer.MultiContainer<TArg, TResult>(comparer, _capacity);
            return () => container;
        }

        public MemoizeBuilder<TArg> Capacity(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentException("capacity is invalid.");
            }

            _capacity = capacity;
            return this;
        }

        public MemoizeBuilder<TArg> Comparer(IEqualityComparer<TArg> comparer)
        {
            _comparer = comparer;
            return this;
        }

        public MemoizeBuilder<TArg> Expiration(TimeSpan expiration)
        {
            _expiration = expiration;
            return this;
        }

        private static Func<TArg, TResult> SynchronizeFunc<TResult>(Func<TArg, TResult> func)
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

        public MemoizeBuilder<TArg> ThreadSafe()
        {
            _threadSafe = true;
            return this;
        }
    }
}
