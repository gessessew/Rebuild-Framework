using System;
using System.Collections.Generic;
using System.Data;

namespace Rebuild.Extensions
{
    public static class DbCommandExtensions
    {
        public static IDbCommand AddParameter(this IDbCommand command, string parameterName, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            command.Parameters.Add(parameter);
            return command;
        }

        public static IEnumerable<IDataReader> ExecuteReaderEnumerable(this IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return reader;
                }
            }
        }

        public static T ExecuteScalar<T>(this IDbCommand command, T defaultValue = default(T))
        {
            var value = command.ExecuteScalar();
            return value == DBNull.Value ? defaultValue : (T)value;
        }

        public static IDbCommand PrepareCommand(this IDbCommand command)
        {
            command.Prepare();
            return command;
        }

        public static IDbCommand SetCommandText(this IDbCommand command, string commandText)
        {
            command.CommandText = commandText;
            return command;
        }

        public static IDbCommand SetParameter(this IDbCommand command, string parameterName, object value)
        {
            var parameter = (IDbDataParameter)command.Parameters[parameterName];

            if (parameter == null)
            {
                parameter = command.CreateParameter();
                parameter.ParameterName = parameterName;
                command.Parameters.Add(parameter);
            }

            parameter.Value = value;
            return command;
        }
    }
}
