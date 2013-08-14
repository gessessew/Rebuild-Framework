using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rebuild.Utils;
using System;

namespace Rebuild.Extensions
{
    [TestClass]
    public class DateTimeOffsetExtensionsTests
    {
        [TestMethod]
        public void Age()
        {
            new DateTime(2000, 1, 1)
                .DateTimeOffsetUtc()
                .Age(new DateTime(2010, 1, 1).DateTimeOffsetUtc())
                .AssertEqual(new Age(10));
        }

        [TestMethod]
        public void Clamp()
        {
            Assert.AreEqual
            (
                new DateTime(2000, 10, 10).DateTimeOffsetUtc(), 

                new DateTime(1999, 1, 1)
                    .DateTimeOffsetUtc()
                    .Clamp(new DateTime(2000, 10, 10).DateTimeOffsetUtc(), new DateTime(2001, 10, 10).DateTimeOffsetUtc())
            );

            Assert.AreEqual
            (
                new DateTime(2001, 10, 10).DateTimeOffsetUtc(),
                
                new DateTime(2002, 1, 1)
                    .DateTimeOffsetUtc()
                    .Clamp(new DateTime(2000, 10, 10).DateTimeOffsetUtc(), new DateTime(2001, 10, 10).DateTimeOffsetUtc())
            );
        }
    }
}
