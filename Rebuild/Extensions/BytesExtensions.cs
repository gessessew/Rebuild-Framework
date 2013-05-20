using System;
using System.IO;
using System.Text;

namespace Rebuild.Extensions
{
    public static partial class BytesExtensions
    {
        public static string ToBase64String(this byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        public static MemoryStream ToMemoryStream(this byte[] buffer)
        {
            return new MemoryStream(buffer);
        }

        public static string ToString(this byte[] buffer, Encoding encoding)
        {
            return (encoding ?? Encoding.UTF8).GetString(buffer, 0, buffer.Length);
        }
    }
}
