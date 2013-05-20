using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rebuild.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void EqualsAnyIgnoreCase()
        {
            "A".EqualsAnyIgnoreCase(default(string), "a").AssertEqual(true);
        }

        [TestMethod]
        public void Left()
        {
            "".Left(1).AssertEqual("");
            "a".Left(1).AssertEqual("a");
            "ab".Left(1).AssertEqual("a");
            "asdrtf".Left(3).AssertEqual("asd");
            "a".Left(2).AssertEqual("a");
        }

        [TestMethod]
        public void Right()
        {
            "".Right(1).AssertEqual("");
            "a".Right(1).AssertEqual("a");
            "ab".Right(1).AssertEqual("b");
            "asdrtf".Right(3).AssertEqual("rtf");
            "a".Right(2).AssertEqual("a");
        }

        [TestMethod]
        public void Substr()
        {
            "abcdefg".Substr(2, 4).AssertEqual("cd");
        }
    }
}
