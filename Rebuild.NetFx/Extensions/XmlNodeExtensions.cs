using System.Xml;
using System.Xml.Linq;

namespace Rebuild.Extensions
{
    public static class XmlNodeExtensions
    {
        public static XElement ToXElement(this XmlNode node)
        {
            return XElement.Parse(node.OuterXml);
        }
    }
}
