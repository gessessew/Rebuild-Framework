using Rebuild.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rebuild.Extensions
{
    public static class TaskExtensions
    {
        private static readonly ConditionalWeakTable<Task, CancellationTokenSource> CancellationTokenSources = new ConditionalWeakTable<Task, CancellationTokenSource>();

        internal static T AddCancellationTokenSource<T>(this T task, CancellationTokenSource cancellationTokenSource) where T : Task
        {
            CancellationTokenSources.Add(task, cancellationTokenSource);
            return task;
        }

        public static Task Cancel(this Task task, int? waitCompleteTimeout = Timeout.Infinite, bool throwIfCannotCancel = true)
        {
            var cts = CancellationTokenSources.GetValue(task, _ => null);
            if (cts == null)
            {
                if (throwIfCannotCancel)
                {
                    throw new InvalidOperationException("Task does not support cancellation.");
                }
            }
            else
            {
                cts.Cancel();
            }

            if (waitCompleteTimeout != null)
            {
                try
                {
                    task.Wait(waitCompleteTimeout.Value);
                }
                catch (AggregateException ex)
                {
                    if (!task.IsCanceled || ex.InnerExceptions.Count != 1 || !(ex.InnerExceptions[0] is OperationCanceledException))
                    {
                        throw ex;
                    }
                }
            }

            return task;
        }

        public static Task<TResult> Cancel<TResult>(this Task<TResult> task, int? waitCompleteTimeout = Timeout.Infinite, bool throwIfCannotCancel = true)
        {
            Cancel((Task)task, waitCompleteTimeout, throwIfCannotCancel);
            return task;
        }

        public static Task<TResult> WaitWithCancellation<TResult>(this Task<TResult> task, int waitCompleteTimeout = Timeout.Infinite, bool throwIfCannotCancel = true)
        {
            var token = Tasks.Cancellation;
            if (!token.CanBeCanceled && throwIfCannotCancel)
                throw new InvalidOperationException("Ambient CancellationToken cannot be found.");

            task.Wait(waitCompleteTimeout, token);
            return task;
        }

        public static Task WaitWithCancellation(this Task task, int waitCompleteTimeout = Timeout.Infinite, bool throwIfCannotCancel = true)
        {
            var token = Tasks.Cancellation;
            if (!token.CanBeCanceled && throwIfCannotCancel)
                throw new InvalidOperationException("Ambient CancellationToken cannot be found.");

            task.Wait(waitCompleteTimeout, token);
            return task;
        }
    }
}
