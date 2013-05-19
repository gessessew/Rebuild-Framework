using System.Xml.Linq;
using Rebuild.Extensions;

namespace Rebuild.Xml
{
    public static class XNamespaces
    {
        public static readonly XNamespace Xsd = XNamespace.Get(XmlNs.Xsd);

        public static readonly XAttribute XsdAttribute = Xsd.ToAttribute("xsd");
    }
}
