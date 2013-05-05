using System.Threading;

namespace Rebuild.Extensions
{
    public static class CancellationTokenExtensions
    {
        public static CancellationToken Combine(this CancellationToken first, CancellationToken second)
        {
            if (first.CanBeCanceled)
            {
                return second.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(first, second).Token : first;
            }

            return second;
        }
    }
}
