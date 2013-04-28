using Rebuild.Extensions;
using System.IO;

namespace Rebuild.IO
{
    public class WrappedStream : Stream
    {
        public WrappedStream(Stream internalStream)
        {
            InternalStream = internalStream;
            IsDisposeInternalStreamOnDispose = true;
        }

        public override bool CanRead
        {
            get { return InternalStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return InternalStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return InternalStream.CanWrite; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (IsDisposeInternalStreamOnDispose)
                InternalStream.DisposeIfNotNull();
        }

        public void DisposeInternalStream()
        {
            InternalStream.DisposeIfNotNull();
        }

        public WrappedStream DisposeInternalStreamOnDispose(bool value)
        {
            IsDisposeInternalStreamOnDispose = value;
            return this;
        }

        public override void Flush()
        {
            InternalStream.Flush();
        }

        public override long Length
        {
            get { return InternalStream.Length; }
        }

        public override long Position
        {
            get { return InternalStream.Position; }
            set { InternalStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return InternalStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return InternalStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            InternalStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            InternalStream.Write(buffer, offset, count);
        }

        public bool IsDisposeInternalStreamOnDispose { get; set; }

        protected Stream InternalStream { get; set; }
    }
}
