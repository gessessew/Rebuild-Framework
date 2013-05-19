using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rebuild.Extensions
{
    [TestClass]
    public class XNodeExtensionsTests
    {
        [TestMethod]
        public void ToXmlDocument()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><test><test1></test1></test>";
            XDocument
                .Parse(xml)
                .ToXmlDocument()
                .OuterXml
                .AssertEqual(xml);

        }
    }
}
