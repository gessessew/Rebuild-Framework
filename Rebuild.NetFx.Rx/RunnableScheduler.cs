using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Threading;

namespace Rebuild.Rx
{
    public sealed class RunnableScheduler : LocalScheduler, ISchedulerPeriodic, IDisposable
    {
        private IConcurrencyAbstractionLayer _concurrencyAbstractionLayer;
        private bool _disposed;
        private readonly SemaphoreSlim _evt;
        private readonly object _gate;
        private ScheduledItem<TimeSpan> _nextItem;
        private readonly SerialDisposable _nextTimer;
        private readonly SchedulerQueue<TimeSpan> _queue;
        private readonly Queue<ScheduledItem<TimeSpan>> _readyList;
        private IStopwatch _stopwatch;

        public RunnableScheduler()
        {
            _concurrencyAbstractionLayer = PlatformEnlightenmentProvider.Current.GetService<IConcurrencyAbstractionLayer>();
            _stopwatch = _concurrencyAbstractionLayer.StartStopwatch();
            _gate = new object();
            _evt = new SemaphoreSlim(0);
            _queue = new SchedulerQueue<TimeSpan>();
            _readyList = new Queue<ScheduledItem<TimeSpan>>();
            _nextTimer = new SerialDisposable();
        }

        public void Dispose()
        {
            lock (_gate)
            {
                if (!_disposed)
                {
                    _disposed = true;
                    _nextTimer.Dispose();
                    _evt.Release();
                }
            }
        }

        public override IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var due = _stopwatch.Elapsed + dueTime;
            var si = new ScheduledItem<TimeSpan, TState>(this, state, action, due);

            lock (_gate)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }

                if (dueTime <= TimeSpan.Zero)
                {
                    _readyList.Enqueue(si);
                }
                else
                {
                    _queue.Enqueue(si);
                }
                _evt.Release();
            }


            return Disposable.Create(si.Cancel);
        }

        public IDisposable SchedulePeriodic<TState>(TState state, TimeSpan period, Func<TState, TState> action)
        {
            if (period < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("period");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var start = _stopwatch.Elapsed;
            var next = start + period;
            var state1 = state;
            var d = new MultipleAssignmentDisposable();
            var gate = new AsyncLock();
            var tick = default(Func<IScheduler, object, IDisposable>);

            tick = (self_, _) =>
            {
                next += period;

                d.Disposable = self_.Schedule(null, next - _stopwatch.Elapsed, tick);

                gate.Wait(() => state1 = action(state1));

                return Disposable.Empty;
            };

            d.Disposable = Schedule(null, next - _stopwatch.Elapsed, tick);

            return new CompositeDisposable(d, gate);
        }

        public void Run(bool exitIfEmpty = true, CancellationToken token = default(CancellationToken))
        {
            var waitHandles = new WaitHandle[] { _evt.AvailableWaitHandle, token.WaitHandle };

            while (true)
            {
                WaitHandle.WaitAny(waitHandles);

                var ready = default(ScheduledItem<TimeSpan>[]);

                lock (_gate)
                {
                    if (_disposed)
                    {
                        ((IDisposable)_evt).Dispose();
                        return;
                    }

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    while (_queue.Count > 0 && _queue.Peek().DueTime <= _stopwatch.Elapsed)
                    {
                        _readyList.Enqueue(_queue.Dequeue());
                    }

                    if (_queue.Count > 0)
                    {
                        var next = _queue.Peek();

                        if (next != _nextItem)
                        {
                            _nextItem = next;
                            _nextTimer.Disposable = _concurrencyAbstractionLayer.StartTimer(Tick, next, next.DueTime - _stopwatch.Elapsed);
                        }
                    }

                    if (_readyList.Count > 0)
                    {
                        ready = _readyList.ToArray();
                        _readyList.Clear();
                    }
                }

                if (ready != null)
                {
                    foreach (var item in ready)
                    {
                        if (!item.IsCanceled)
                        {
                            item.Invoke();
                        }
                    }
                }

                if (exitIfEmpty)
                {
                    lock (_gate)
                    {
                        if (_readyList.Count == 0 && _queue.Count == 0)
                        {
                            return;
                        }
                    }
                }
            }
        }

        public void RunUntilCompleted<T>(IObservable<T> observable)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            observable
                .SubscribeOn(TaskPoolScheduler.Default)
                .Finally(cancellationTokenSource.Cancel)
                .Subscribe();

            Run(false, cancellationTokenSource.Token);
        }

        private void Tick(object state)
        {
            lock (_gate)
            {
                if (!_disposed)
                {
                    var item = (ScheduledItem<TimeSpan>)state;

                    if (_queue.Remove(item))
                    {
                        _readyList.Enqueue(item);
                    }

                    _evt.Release();
                }
            }
        }
    }
}
