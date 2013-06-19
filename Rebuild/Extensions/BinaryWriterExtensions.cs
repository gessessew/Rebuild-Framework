using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rebuild.Extensions
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, bool? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, byte? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, char? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, DateTime value)
        {
            writer.Write(value.Ticks);
            writer.Write((byte)value.Kind);
        }

        public static void Write(this BinaryWriter writer, DateTime? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, DateTimeOffset value)
        {
            writer.Write(value.Ticks);
            writer.Write(value.Offset);
        }

        public static void Write(this BinaryWriter writer, DateTimeOffset? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, decimal value)
        {
            var values = Decimal.GetBits(value);
            writer.Write(values[0]);
            writer.Write(values[1]);
            writer.Write(values[2]);
            writer.Write(values[3]);
        }

        public static void Write(this BinaryWriter writer, decimal? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, float? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, Guid value)
        {
            writer.Write(value.ToByteArray());
        }

        public static void Write(this BinaryWriter writer, Guid? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, int? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, long? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, sbyte? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, short? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, TimeSpan value)
        {
            writer.Write(value.Ticks);
        }

        public static void Write(this BinaryWriter writer, TimeSpan? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, ushort? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, uint? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        public static void Write(this BinaryWriter writer, ulong? value)
        {
            WriteInternal(writer, value, writer.Write);
        }

        private static void WriteInternal<T>(BinaryWriter writer, T? value, Action<T> write) where T : struct
        {
            writer.Write(value != null);

            if (value != null)
            {
                write(value.Value);
            }
        }

        public static void WriteStringNullable(this BinaryWriter writer, string value)
        {
            writer.Write(value != null);

            if (value != null)
            {
                writer.Write(value);
            }
        }
    }
}
