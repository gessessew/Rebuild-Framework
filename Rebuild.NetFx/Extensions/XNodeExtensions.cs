using System.Xml;
using System.Xml.Linq;

namespace Rebuild.Extensions
{
    public static class XNodeExtensions
    {
        public static XmlDocument ToXmlDocument(this XNode node)
        {
            using (var reader = node.CreateReader())
            {
                var xml = new XmlDocument();
                xml.Load(reader);

                var doc = node as XDocument;
                var dec = doc == null ? null : doc.Declaration;

                if (dec != null)
                {
                    xml.InsertBefore(
                        xml.CreateXmlDeclaration(dec.Version, dec.Encoding, dec.Standalone),
                        xml.FirstChild);
                }
                
                return xml;
            }
        }
    }
}
