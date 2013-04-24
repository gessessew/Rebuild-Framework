using Rebuild.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Rebuild.Wcf
{
    public sealed class InternationalizationAttribute : Attribute, IOperationBehavior, IClientMessageInspector, IDispatchMessageInspector, IEndpointBehavior, IServiceBehavior
    {
        public string Locale { get; set; }
        public string TimeZone { get; set; }

        void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            endpoint.Behaviors.AddIfNotExists(this);
        }

        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            foreach (var endpoint in endpoints)
            {
                endpoint.Behaviors.AddIfNotExists(this);
            }
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.AddIfNotExists(this);
        }

        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var index = request.Headers.FindHeader(WsI18N.ElementNames.International, WsI18N.NamespaceURI);
            if (index > -1)
            {
                request.Headers.UnderstoodHeaders.Add(request.Headers[index]);

                var header = request.Headers.GetHeader<International>(index);
                if (header.Locale.HasValue())
                    SetCurrentCulture(header.Locale);
            }

            return null;
        }

        void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.Parent.MessageInspectors.AddIfNotExists(this);
        }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Parent.MessageInspectors.AddIfNotExists(this);
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
        }

        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var header = MessageHeader.CreateHeader
            (
                WsI18N.ElementNames.International,
                WsI18N.NamespaceURI,
                new International { Locale = Locale, Tz = TimeZone }
            );
            request.Headers.Add(header);
            return null;
        }

        private static void SetCurrentCulture(string cultureName)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            }
            catch
            {
            }
        }

        void IOperationBehavior.Validate(OperationDescription operationDescription)
        {
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }

    #region class International
    [DataContract(Name = WsI18N.ElementNames.International, Namespace = WsI18N.NamespaceURI)]
    public class International
    {
        [DataMember(Name = WsI18N.ElementNames.Locale)] 
        public string Locale { get; set; }

        [DataMember(Name = WsI18N.ElementNames.TZ)] 
        public string Tz { get; set; }

        [DataMember(Name = WsI18N.ElementNames.Preferences)]
        public List<Preferences> Preferences { get; set; }
    }
    #endregion

    #region class Preferences
    public class Preferences : IXmlSerializable
    {
        public XmlNode AnyElement { get; set; }    
        
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            AnyElement = new XmlDocument().ReadNode(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            AnyElement.WriteTo(writer);
        }
    }
    #endregion

    #region class WsI18N
    internal static class WsI18N
    {
        public const string Prefix = "i18n";
        public const string NamespaceURI = "http://www.w3.org/2005/09/ws-i18n";

        public static class ElementNames
        {
            public const string International = "International";
            public const string Locale = "Locale";
            public const string TZ = "TZ";
            public const string Preferences = "Preferences";
        }
    }
    #endregion
}