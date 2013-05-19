using System.Xml;
using System.Xml.Linq;

namespace Rebuild.Extensions
{
    public static class XmlNamespaceManagerExtensions
    {
        public static XmlNamespaceManager AddNs(this XmlNamespaceManager manager, string prefix, string uri)
        {
            manager.AddNamespace(prefix, uri);
            return manager;
        }

        public static XmlNamespaceManager AddNs(this XmlNamespaceManager manager, XAttribute nsAttribute)
        {
            manager.AddNamespace(nsAttribute.Name.LocalName, nsAttribute.Value);
            return manager;
        }

        public static XmlNamespaceManager AddNs(this XmlNamespaceManager manager, params XAttribute[] nsAttributes)
        {
            if (nsAttributes != null)
            {
                foreach (var a in nsAttributes)
                {
                    manager.AddNamespace(a.Name.LocalName, a.Value);
                }
            }
            
            return manager;
        }
    }
}
