using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebuild.Extensions
{
    public static class StackExtensions
    {
        public static PushRangeResult<T> PushRange<T>(this Stack<T> stack, IEnumerable<T> itemsToAdd)
        {
            return PushRange(stack, itemsToAdd.EmptyIfNull().ToArray());
        }

        public static PushRangeResult<T> PushRange<T>(this Stack<T> stack, params T[] itemsToAdd)
        {
            if (itemsToAdd == null)
            {
                itemsToAdd = new T[0];
            }

            for (var i = itemsToAdd.Length - 1; i > -1; i--)
            {
                stack.Push(itemsToAdd[i]);
            }

            return new PushRangeResult<T>(stack, itemsToAdd);
        }
    }
}
