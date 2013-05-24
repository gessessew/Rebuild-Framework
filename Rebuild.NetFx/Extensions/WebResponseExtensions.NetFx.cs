using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Rebuild.Extensions
{
    public static partial class WebResponseExtensions
    {
        public static XmlDocument ToXml(this WebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                var document = new XmlDocument();
                document.Load(stream);
                return document;
            }
        }
    }
}
