using System.Net;
using System.Xml.Linq;

namespace Rebuild.Extensions
{
    public static partial class WebResponseExtensions
    {
        public static XDocument ToXDocument(this WebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                return XDocument.Load(stream);
            }
        }
    }
}
