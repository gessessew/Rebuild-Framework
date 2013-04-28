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

        public static WrappedStream WrapStream(this Stream stream)
        {
            return new WrappedStream(stream);
        }
    }
}
