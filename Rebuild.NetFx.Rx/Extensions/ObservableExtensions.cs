using Rebuild.Rx;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Rebuild.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<T> EmptyIfNull<T>(this IObservable<T> source, IScheduler scheduler = null)
        {
            return source ?? Observable.Empty<T>(scheduler);
        }

        public static IObservable<T> NeverIfNull<T>(this IObservable<T> source)
        {
            return source ?? Observable.Never<T>();
        }

        public static IObservable<T> ReturnIfNull<T>(this IObservable<T> source, IScheduler scheduler = null, T defaultValue = default(T))
        {
            return source ?? Observable.Return<T>(defaultValue, scheduler);
        }

        public static void RunUntilCompleted<T>(this IObservable<T> source, RunnableScheduler scheduler)
        {
            scheduler.RunUntilCompleted(source);
        }
    }
}
