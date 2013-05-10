using System;
using System.Collections.Generic;

namespace Rebuild.Utils
{
    public static partial class Memoizer
    {
        public static Func<TArg, TResult> Memoize<TArg, TResult>(Func<TArg, TResult> factory, Func<MemoizeBuilder<TArg, TResult>, MemoizeBuilder<TArg, TResult>> builder)
        {
            return builder(new MemoizeBuilder<TArg, TResult>()).Build(factory);
        }

        public static Func<TArg, TResult> Memoize<TArg, TResult>(Func<TArg, TResult> factory, IEqualityComparer<TArg> comparer = null)
        {
            var container = new MultiContainer<TArg, TResult>(comparer ?? EqualityComparer<TArg>.Default);
            return MemoizeInternal(factory, null, int.MaxValue, () => container);
        }

        internal static Func<TArg, TResult> MemoizeInternal<TArg, TResult>(Func<TArg, TResult> factory, Func<TArg, TResult, DateTime, bool> expiration, int capacity, Func<MultiContainer<TArg, TResult>> containerProvider)
        {
            return arg =>
            {
                var container = containerProvider();

                return container.Dic.Comparer.Equals(arg, default(TArg))
                    ? MemoizeDefault(factory, expiration, capacity, arg, container)
                    : MemoizeValues(factory, expiration, capacity, arg, container);
            };
        }

        private static TResult MemoizeDefault<TArg, TResult>(Func<TArg, TResult> factory, Func<TArg, TResult, DateTime, bool> expiration, int capacity, TArg arg, MultiContainer<TArg, TResult> container)
        {
            if (!container.DefaultValue.HasValue || (expiration != null && expiration(arg, container.DefaultValue.Value.Value, container.DefaultValue.Value.Time)))
            {
                if (!container.DefaultValue.HasValue && capacity <= container.Count)
                {
                    var value = default(Record<TResult>?);
                    RemoveOldestItem(container.Dic, ref value);
                }

                container.DefaultValue = new Record<TResult>
                {
                    Value = factory(arg),
                    Time = DateTime.UtcNow
                };
            }

            return container.DefaultValue.Value.Value;
        }

        public static Func<TArg, TResult> MemoizeOne<TArg, TResult>(Func<TArg, TResult> factory, IEqualityComparer<TArg> comparer = null)
        {
            var container = new SingleContainer<TArg, TResult>();
            return MemoizeOneInternal(factory, null, comparer ?? EqualityComparer<TArg>.Default, () => container);
        }

        internal static Func<TArg, TResult> MemoizeOneInternal<TArg, TResult>(Func<TArg, TResult> factory, Func<TArg, TResult, DateTime, bool> expiration, IEqualityComparer<TArg> comparer, Func<SingleContainer<TArg, TResult>> containerProvider)
        {
            return arg =>
            {
                var container = containerProvider();

                if (!container.Record.HasValue ||
                    (expiration != null && expiration(arg, container.Record.Value.Value, container.Record.Value.Time)) ||
                    !comparer.Equals(container.Key, arg))
                {
                    container.Record = new Record<TResult>
                    {
                        Value = factory(arg),
                        Time = DateTime.UtcNow
                    };

                    container.Key = arg;
                }
                
                return container.Record.Value.Value;
            };
        }

        private static TResult MemoizeValues<TArg, TResult>(Func<TArg, TResult> factory, Func<TArg, TResult, DateTime, bool> expiration, int capacity, TArg arg, MultiContainer<TArg, TResult> container)
        {
            Record<TResult> record;
            var hasValue = container.Dic.TryGetValue(arg, out record);

            if (!hasValue || (expiration != null && expiration(arg, record.Value, record.Time)))
            {
                if (!hasValue && capacity <= container.Count)
                {
                    RemoveOldestItem(container.Dic, ref container.DefaultValue);
                }

                record = new Record<TResult>
                {
                    Value = factory(arg),
                    Time = DateTime.UtcNow
                };

                container.Dic[arg] = record;
            }

            return record.Value;
        }

        private static void RemoveOldestItem<TArg, TResult>(Dictionary<TArg, Record<TResult>> dic, ref Record<TResult>? value)
        {
            var d = default(DateTime?);
            var key = default(TArg);

            foreach (var kv in dic)
            {
                if (d == null || d.Value > kv.Value.Time)
                {
                    d = kv.Value.Time;
                    key = kv.Key;
                }
            }

            if (d == null || (value != null && d.Value > value.Value.Time))
            {
                value = null;
            }
            else
            {
                dic.Remove(key);
            }
        }

        #region class SingleContainer<TKey, TValue>
        internal sealed class SingleContainer<TKey, TValue>
        {
            internal TKey Key;
            internal Record<TValue>? Record;
        }
        #endregion

        #region class MultiContainer<TKey, TValue>
        internal sealed class MultiContainer<TKey, TValue>
        {
            internal MultiContainer(IEqualityComparer<TKey> comparer)
            {
                Dic = new Dictionary<TKey, Record<TValue>>(comparer);
            }

            internal int Count
            {
                get { return Dic.Count + (DefaultValue.HasValue ? 1 : 0); }
            }

            internal Record<TValue>? DefaultValue;

            internal readonly Dictionary<TKey, Record<TValue>> Dic;
        }
        #endregion

        #region struct Record<TValue>
        internal struct Record<TValue>
        {
            internal DateTime Time;

            internal TValue Value;
        }
        #endregion
    }
}
