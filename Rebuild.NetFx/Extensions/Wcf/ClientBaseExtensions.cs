using Rebuild.Wcf;
using System.Globalization;
using System.ServiceModel;

namespace Rebuild.Extensions.Wcf
{
    public static class ClientBaseExtensions
    {
        public static ClientBase<TChannel> InternationalizeClient<TChannel>(this ClientBase<TChannel> client, CultureInfo culture = null, string timeZone = null) where TChannel : class
        {
            return client.InternationalizeClient((culture ?? CultureInfo.CurrentUICulture).Name, timeZone);
        }

        public static ClientBase<TChannel> InternationalizeClient<TChannel>(this ClientBase<TChannel> client, string locale, string timeZone = null) where TChannel : class
        {
            client
                .Endpoint
                .Behaviors
                .Add(new InternationalizationAttribute
                {
                    Locale = locale ?? CultureInfo.CurrentUICulture.Name,
                    TimeZone = timeZone
                });

            return client;
        }
    }
}
