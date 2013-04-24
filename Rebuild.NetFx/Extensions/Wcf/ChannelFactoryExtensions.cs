using Rebuild.Wcf;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Rebuild.Extensions.Wcf
{
    public static class ChannelFactoryExtensions
    {
        public static T Bind<T>(this T factory, Binding binding) where T : ChannelFactory
        {
            factory.Endpoint.Binding = binding;
            return factory;
        }

        public static T Credentials<T>(this T factory, string username, string password) where T : ChannelFactory
        {
            factory.Credentials.UserName.UserName = username;
            factory.Credentials.UserName.Password = password;
            return factory;
        }

        public static T Endpoint<T>(this T factory, string address) where T : ChannelFactory
        {
            return factory.Endpoint(new EndpointAddress(address));
        }

        public static T Endpoint<T>(this T factory, EndpointAddress address) where T : ChannelFactory
        {
            factory.Endpoint.Address = address;
            return factory;
        }

        public static T Internationalize<T>(this T channelFactory, CultureInfo culture = null, string timeZone = null) where T : ChannelFactory
        {
            return channelFactory.Internationalize((culture ?? CultureInfo.CurrentUICulture).Name, timeZone);
        }

        public static T Internationalize<T>(this T channelFactory, string locale, string timeZone = null) where T : ChannelFactory
        {
            channelFactory
                .Endpoint
                .Behaviors
                .Add(new InternationalizationAttribute
                {
                    Locale = locale ?? CultureInfo.CurrentUICulture.Name,
                    TimeZone = timeZone
                });

            return channelFactory;
        }
    }
}
