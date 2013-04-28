using System;

namespace Rebuild.Extensions
{
    public static class DisposeExtensions
    {
        public static void DisposeIfNotNull(this IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
