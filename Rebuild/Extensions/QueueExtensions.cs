using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    public static class QueueExtensions
    {
        public static EnqueueRangeResult<T> EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> itemsToAdd)
        {
            return EnqueueRange(queue, itemsToAdd.EmptyIfNull().ToArray());
        }

        public static EnqueueRangeResult<T> EnqueueRange<T>(this Queue<T> queue, params T[] itemsToAdd)
        {
            if (itemsToAdd == null)
            {
                itemsToAdd = new T[0];
            }

            for (var i = itemsToAdd.Length - 1; i > -1 ; i--)
            {
                queue.Enqueue(itemsToAdd[i]);
            }

            return new EnqueueRangeResult<T>(queue, itemsToAdd);
        }
    }
}
