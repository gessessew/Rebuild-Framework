using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebuild.Extensions
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void AddIfNotExists()
        {
            new List<int> { 1, 2 }
                .AddIfNotExists(1)
                .AddIfNotExists(2);
        }
    }
}
