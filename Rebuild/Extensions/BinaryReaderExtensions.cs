using System;
using System.IO;

namespace Rebuild.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static bool? ReadBooleanNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadBoolean() : (bool?)null;
        }

        public static byte? ReadByteNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadByte() : (byte?)null;
        }

        public static char? ReadCharNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadChar() : (char?)null;
        }

        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            return new DateTime(reader.ReadInt64(), (DateTimeKind)reader.ReadByte());
        }

        public static DateTime? ReadDateTimeNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadDateTime() : (DateTime?)null;
        }

        public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
        {
            return new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
        }

        public static DateTimeOffset? ReadDateTimeOffsetNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadDateTimeOffset() : (DateTimeOffset?)null;
        }

        public static decimal ReadDecimal(this BinaryReader reader)
        {
            return new decimal(new[] { reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32() });
        }

        public static decimal? ReadDecimalNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadDecimal() : (decimal?)null;
        }    

        public static double? ReadDoubleNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadDouble() : (double?)null;
        }

        public static Guid ReadGuid(this BinaryReader reader)
        {
            return new Guid(reader.ReadBytes(16));
        }

        public static Guid? ReadGuidNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadGuid() : (Guid?)null;
        }

        public static long? ReadInt16Nullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadInt16() : (short?)null;
        }

        public static int? ReadInt32Nullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadInt32() : (int?)null;
        }

        public static long? ReadInt64Nullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadInt64() : (long?)null;
        }

        public static sbyte? ReadSByteNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadSByte() : (sbyte?)null;
        }

        public static float? ReadSingleNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadSingle() : (float?)null;
        }

        public static string ReadStringNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadString() : null;
        }

        public static TimeSpan ReadTimeSpan(this BinaryReader reader)
        {
            return new TimeSpan(reader.ReadInt64());
        }

        public static TimeSpan? ReadTimeSpanNullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadTimeSpan() : (TimeSpan?)null;
        }

        public static ushort? ReadUInt16Nullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadUInt16() : (ushort?)null;
        }

        public static uint? ReadUInt32Nullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadUInt32() : (uint?)null;
        }

        public static ulong? ReadUInt64Nullable(this BinaryReader reader)
        {
            return reader.ReadBoolean() ? reader.ReadUInt64() : (ulong?)null;
        }
    }
}
