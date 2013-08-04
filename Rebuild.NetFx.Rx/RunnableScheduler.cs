using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace Rebuild.Rx
{
    public sealed class RunnableScheduler : IScheduler, ISchedulerPeriodic, IDisposable
    {
        private readonly ManualResetEventSlim _resetEvent;
        private readonly EventLoopScheduler _scheduler;
        private ThreadStart _threadStart;

        public RunnableScheduler()
        {
            _resetEvent = new ManualResetEventSlim();
            _scheduler = new EventLoopScheduler(ts =>
            {
                _threadStart = ts;
                _resetEvent.Set();
                return new Thread(() => { });
            });
        }

        public void Dispose()
        {
            _scheduler.Schedule(_scheduler.Dispose);
            _resetEvent.Dispose();
        }

        public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            return _scheduler.Schedule(state, dueTime, action);
        }

        public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action)
        {
            return _scheduler.Schedule(state, action);
        }

        public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            return _scheduler.Schedule(state, dueTime, action);
        }

        public IDisposable SchedulePeriodic<TState>(TState state, TimeSpan period, Func<TState, TState> action)
        {
            return _scheduler.SchedulePeriodic(state, period, action);
        }

        public void Run()
        {
            _resetEvent.Wait();
            _threadStart();
        }

        public void RunUntilCompleted<T>(IObservable<T> observable)
        {
            Exception exception = null;

            observable
                .Finally(Dispose)
                .Subscribe(_ => { }, ex => exception = ex);

            Run();

            if (exception != null)
            {
                throw exception;
            }
        }

        public DateTimeOffset Now
        {
            get { return _scheduler.Now; }
        }
    }
}
