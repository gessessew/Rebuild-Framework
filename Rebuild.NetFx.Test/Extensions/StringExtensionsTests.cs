using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Rebuild.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void RemoveDiacritics()
        {
            "àïôé".RemoveDiacritics().AssertEqual("aioe");
        }

        [TestMethod]
        public void SerializeDeserializeJson()
        {
            new SampleObj { Value1 = "123", Value2 = 456 }
                .SerializeToJsonString()
                .DeserializeJson<SampleObj>()
                .With(o => o.Value1.AssertEqual("123"))
                .With(o => o.Value2.AssertEqual(456));
        }

        #region class SampleObj
        public class SampleObj
        {
            public string Value1 { get; set; }
            public int Value2 { get; set; }
        }
        #endregion
    }
}
