using System.Xml.Linq;

namespace Rebuild.Extensions
{
    public static class XNamespaceExtensions
    {
        public static XAttribute ToAttribute(this XNamespace ns, string prefix)
        {
            return new XAttribute(XNamespace.Xmlns + prefix, ns.NamespaceName);
        }
    }
}
