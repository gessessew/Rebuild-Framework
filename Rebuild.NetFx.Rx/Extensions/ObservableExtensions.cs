using Rebuild.Rx;
using System;

namespace Rebuild.Extensions
{
    public static class ObservableExtensions
    {
        public static void RunUntilCompleted<T>(this IObservable<T> source, RunnableScheduler scheduler)
        {
            scheduler.RunUntilCompleted(source);
        }
    }
}
