using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebuild.Extensions
{
    [TestClass]
    public class AssemblyExtensionsTests
    {
        [TestMethod]
        public void GetCompany()
        {
            typeof(AssemblyExtensionsTests)
                .Assembly
                .GetCompany()
                .AssertEqual("Rebuild");
        }
    }
}
