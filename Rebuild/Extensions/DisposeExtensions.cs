using System;

namespace Rebuild.Extensions
{
    public static class DisposeExtensions
    {
        public static void SafeDispose(this IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
