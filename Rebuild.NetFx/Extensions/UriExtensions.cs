using Rebuild.Utils;
using System;

namespace Rebuild.Extensions
{
    public static class UriExtensions
    {
        public static QueryStringBuilder ToQueryString(this Uri uri)
        {
            return uri.ToString().ToQueryString();
        }
    }
}
