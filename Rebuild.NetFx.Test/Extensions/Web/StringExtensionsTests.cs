using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rebuild.Extensions;

namespace Rebuild.Extensions.Web
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ToQueryString()
        {
            "http://test.com/"
                .ToQueryString()
                .AddParameter("Param1", "Value1")
                .AddParameter("Param2", "Value2")
                .ToString()
                .AssertEqual("http://test.com?Param1=Value1&Param2=Value2");

            "http://test.com?Param1=Value1"
                .ToQueryString()
                .AddParameter("Param2", "Value2")
                .ToString()
                .AssertEqual("http://test.com?Param1=Value1&Param2=Value2");
        }
    }
}
