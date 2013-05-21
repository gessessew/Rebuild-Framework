using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebuild.Extensions
{
    [TestClass]
    public class Int32ExtensionsTests
    {
        [TestMethod]
        public void CombineHash()
        {
            10.CombineHash(2).AssertEqual(312);
            10.CombineHash(default(string)).AssertEqual(10);
        }
    }
}
