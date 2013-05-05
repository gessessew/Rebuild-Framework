using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rebuild.Extensions;

namespace Rebuild.Utils
{
    [TestClass]
    public class MemoizerTests
    {
        [TestMethod]
        public void Memoize()
        {
            int i = 1;
            var a = new object();
            var b = new object();

            var func = Memoizer.Memoize((object o) => i++);

            func(a).AssertEqual(1);
            func(b).AssertEqual(2);
            func(a).AssertEqual(1);
            func(b).AssertEqual(2);

            func(null).AssertEqual(3);
            func(null).AssertEqual(3);
        }

        [TestMethod]
        public void MemoizeOne()
        {
            int i = 1;
            var a = new object();
            var b = new object();

            var func = Memoizer.MemoizeOne((object o) => i++);

            func(a).AssertEqual(1);
            func(a).AssertEqual(1);
            func(b).AssertEqual(2);
            func(b).AssertEqual(2);
            func(a).AssertEqual(3);

            func(null).AssertEqual(4);
            func(null).AssertEqual(4);

        }
    }
}
