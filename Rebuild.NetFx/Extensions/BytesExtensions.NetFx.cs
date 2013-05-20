using System.Web;

namespace Rebuild.Extensions
{
    partial class BytesExtensions
    {
        public static string UrlEncode(this byte[] bytes)
        {
            return HttpUtility.UrlEncode(bytes);
        }
    }
}
