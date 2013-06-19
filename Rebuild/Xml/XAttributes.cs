using Rebuild.Extensions;
using System.Xml.Linq;

namespace Rebuild.Xml
{
    public static class XAttributes
    {
        public static readonly XAttribute SoapAttribute = XNamespaces.Soap.ToAttribute("soap");
        public static readonly XAttribute XsAttribute = XNamespaces.Xs.ToAttribute("xs");
        public static readonly XAttribute XsdAttribute = XNamespaces.Xsd.ToAttribute("xsd");
        public static readonly XAttribute XsiAttribute = XNamespaces.Xsi.ToAttribute("Xsi");
        public static readonly XAttribute XslAttribute = XNamespaces.Xsl.ToAttribute("xsl");
    }
}
