using System.Configuration;
using System.Data.Common;

namespace Rebuild.Extensions
{
    public static class ConfigurationExtensions
    {
        public static DbConnection CreateConnection(this ConnectionStringSettings setting)
        {
            var connection = DbProviderFactories
                .GetFactory(setting.ProviderName)
                .CreateConnection();

            connection.ConnectionString = setting.ConnectionString;
            return connection;
        }
    }
}
