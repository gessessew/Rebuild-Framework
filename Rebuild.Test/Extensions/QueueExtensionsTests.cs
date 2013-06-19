using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebuild.Extensions
{
    [TestClass]
    public class QueueExtensionsTests
    {
        [TestMethod]
        public void EnqueueRange()
        {
            var a = new[] { 1, 2, 3 };

            new Queue<int>()
                .EnqueueRange(a)
                .Queue.SequenceEqual(new Queue<int>(a));

            new Queue<int>()
                .EnqueueRange(a.AsEnumerable())
                .Queue.SequenceEqual(new Queue<int>(a));
        }
    }
}
