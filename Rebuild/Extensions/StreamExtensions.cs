using Rebuild.IO;
using System.IO;

namespace Rebuild.Extensions
{
    public static class StreamExtensions
    {
        public static Stream Clear(this Stream stream)
        {
            stream.SetLength(0);
            return stream;
        }

        public static Stream ResetPosition(this Stream stream)
        {
            stream.Position = 0;
            return stream;
        }

        public static byte[] ToArray(this Stream stream)
        {
            var data = new byte[stream.Length];

            stream.Read(data, 0, data.Length);

            return data;
        }

        public static WrappedStream WrapStream(this Stream stream)
        {
            return new WrappedStream(stream);
        }
    }
}
