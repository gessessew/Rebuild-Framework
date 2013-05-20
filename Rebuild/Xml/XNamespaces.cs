using System.Xml.Linq;
using Rebuild.Extensions;

namespace Rebuild.Xml
{
    public static class XNamespaces
    {
        public static readonly XNamespace Soap = XNamespace.Get(XmlNs.Soap);
        public static readonly XNamespace XHtml = XNamespace.Get(XmlNs.XHtml);
        public static readonly XNamespace Xs = XNamespace.Get(XmlNs.Xs);
        public static readonly XNamespace Xsd = XNamespace.Get(XmlNs.Xsd);
        public static readonly XNamespace Xsi = XNamespace.Get(XmlNs.Xsi);
        public static readonly XNamespace Xsl = XNamespace.Get(XmlNs.Xsl);

        public static readonly XAttribute SoapAttribute = Soap.ToAttribute("soap");
        public static readonly XAttribute XsAttribute = Xsd.ToAttribute("xs");
        public static readonly XAttribute XsdAttribute = Xsd.ToAttribute("xsd");
        public static readonly XAttribute XsiAttribute = Xsd.ToAttribute("Xsi");
        public static readonly XAttribute XslAttribute = Xsl.ToAttribute("xsl");
    }
}
