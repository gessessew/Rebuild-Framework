using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebuild.Extensions
{ 
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void Concat()
        {
            new[] { 1, 2 }.Concat(3, 4).AssertSequenceEqual(1, 2, 3, 4);
        }

        [TestMethod]
        public void ContainsAll()
        {
            var array = new[] { 1, 2, 3 };
            array.ContainsAll(1, 3).AssertEqual(true);
            array.ContainsAll(1, 5).AssertEqual(false);
        }

        [TestMethod]
        public void ContainsAny()
        {
            var array = new[] { 1, 2, 3 };
            array.ContainsAny(1, 0).AssertEqual(true);
            array.ContainsAny(0, 4).AssertEqual(false);
        }

        [TestMethod]
        public void IndexOfAny()
        {
            var array = new[] { 1, 2, 3 };
            array.IndexOfAny(1, 0).AssertEqual(0);
            array.IndexOfAny(0, 4).AssertEqual(-1);
        }

        [TestMethod]
        public void LastIndexOf()
        {
            var array = new[] { 1, 2, 1, 2 };
            array.LastIndexOf(1).AssertEqual(2);
            array.LastIndexOf(2).AssertEqual(3);
            array.LastIndexOf(3).AssertEqual(-1);

            array.LastIndexOf(v => v == 1).AssertEqual(2);
            array.LastIndexOf(v => v == 2).AssertEqual(3);
            array.LastIndexOf(v => v == 3).AssertEqual(-1);
        }

        [TestMethod]
        public void LastIndexOfAny()
        {
            var array = new[] { 1, 2, 1 };
            array.LastIndexOfAny(1, 0).AssertEqual(2);
            array.LastIndexOfAny(0, 4).AssertEqual(-1);
        }
    }
}
