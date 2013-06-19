using Rebuild.Utils;
using System;
using System.Collections.Generic;
using System.Data;

namespace Rebuild.Extensions
{
    public static class DataReaderExtensions
    {
        public static IEnumerable<DataReaderFieldInfo> FieldInfos(this IDataReader dataReader, bool dispose = false)
        {
            using (dispose ? dataReader : null)
            {
                var count = dataReader.FieldCount;

                for (var i = 0; i < count; i++)
                {
                    yield return new DataReaderFieldInfo(i, dataReader.GetName(i), dataReader.GetFieldType(i));
                }
            }
        }

        public static byte GetByteOrDefault(this IDataReader reader, int i, byte defaultValue = 0)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetByte(i);
        }

        public static byte? GetByteOrDefault(this IDataReader reader, int i, byte? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetByte(i);
        }

        public static char GetCharOrDefault(this IDataReader reader, int i, char defaultValue = default(char))
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetChar(i);
        }

        public static char? GetCharOrDefault(this IDataReader reader, int i, char? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetChar(i);
        }

        public static DateTime GetDateTimeOrDefault(this IDataReader reader, int i, DateTime defaultValue = default(DateTime))
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetDateTime(i);
        }

        public static DateTime? GetDateTimeOrDefault(this IDataReader reader, int i, DateTime? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetDateTime(i);
        }

        public static decimal GetDecimalOrDefault(this IDataReader reader, int i, decimal defaultValue = 0)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetDecimal(i);
        }

        public static decimal? GetDecimalOrDefault(this IDataReader reader, int i, decimal? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetDecimal(i);
        }

        public static double GetDoubleOrDefault(this IDataReader reader, int i, double defaultValue = 0)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetDouble(i);
        }

        public static double? GetDoubleOrDefault(this IDataReader reader, int i, double? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetDouble(i);
        }

        public static float GetFloatOrDefault(this IDataReader reader, int i, float defaultValue = 0)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetFloat(i);
        }

        public static float? GetFloatOrDefault(this IDataReader reader, int i, float? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetFloat(i);
        }

        public static Guid GetGuidOrDefault(this IDataReader reader, int i, Guid defaultValue = default(Guid))
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetGuid(i);
        }

        public static Guid? GetGuidOrDefault(this IDataReader reader, int i, Guid? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetGuid(i);
        }

        public static short GetInt16OrDefault(this IDataReader reader, int i, short defaultValue = 0)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetInt16(i);
        }

        public static short? GetInt16OrDefault(this IDataReader reader, int i, short? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetInt16(i);
        }

        public static int GetInt32OrDefault(this IDataReader reader, int i, int defaultValue = 0)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetInt32(i);
        }

        public static int? GetInt32OrDefault(this IDataReader reader, int i, int? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetInt32(i);
        }

        public static long GetInt64OrDefault(this IDataReader reader, int i, long defaultValue = 0)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetInt64(i);
        }

        public static long? GetInt64OrDefault(this IDataReader reader, int i, long? defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetInt64(i);
        }

        public static string GetStringOrDefault(this IDataReader reader, int i, string defaultValue = null)
        {
            return reader.IsDBNull(i) ? defaultValue : reader.GetString(i);
        }

        public static IEnumerable<IDataReader> ToEnumerable(this IDataReader dataReader, bool dispose = true)
        {
            using (dispose ? dataReader : null)
            {
                while (dataReader.Read())
                {
                    yield return dataReader;
                }
            }
        }
    }
}
