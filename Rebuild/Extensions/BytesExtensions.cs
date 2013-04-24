using System.Text;

namespace Rebuild.Extensions
{
    public static class BytesExtensions
    {
        public static string Encode(this byte[] buffer, Encoding encoding)
        {
            return encoding.GetString(buffer, 0, buffer.Length);
        }

        public static string ToUnicode(this byte[] buffer)
        {
            return buffer.Encode(Encoding.Unicode);
        }

        public static string ToUtf8(this byte[] buffer)
        {
            return buffer.Encode(Encoding.UTF8);
        }
    }
}
