using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rebuild.Extensions
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void Age()
        {
            new DateTime(2000, 1, 1)
                .Age(new DateTime(2010, 1, 1))
                .AssertEqual(new DateTime(10, 1, 1));
        }

        [TestMethod]
        public void Clamp()
        {
            new DateTime(1999,1,1)
                .Clamp(new DateTime(2000,10,10), new DateTime(2001, 10, 10))
                .AssertEqual(new DateTime(2000, 10, 10));

            new DateTime(2002, 1, 1)
                .Clamp(new DateTime(2000, 10, 10), new DateTime(2001, 10, 10))
                .AssertEqual(new DateTime(2001, 10, 10));
        }

        [TestMethod]
        public void NextDay()
        {
            Assert.AreEqual(new DateTime(2013, 4, 10), new DateTime(2013, 4, 3).NextDay(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2013, 4, 10), new DateTime(2013, 4, 4).NextDay(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2013, 4, 3), new DateTime(2013, 4, 2).NextDay(DayOfWeek.Wednesday));

            Assert.AreEqual(new DateTime(2013, 4, 14), new DateTime(2013, 4, 7).NextDay(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 4, 14), new DateTime(2013, 4, 8).NextDay(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 4, 7), new DateTime(2013, 4, 6).NextDay(DayOfWeek.Sunday));

            Assert.AreEqual(new DateTime(2013, 4, 20), new DateTime(2013, 4, 13).NextDay(DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2013, 4, 20), new DateTime(2013, 4, 14).NextDay(DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2013, 4, 13), new DateTime(2013, 4, 12).NextDay(DayOfWeek.Saturday));
        }

        [TestMethod]
        public void PreviousDay()
        {
            Assert.AreEqual(new DateTime(2013, 4, 3), new DateTime(2013, 4, 10).PreviousDay(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2013, 4, 10), new DateTime(2013, 4, 11).PreviousDay(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2013, 4, 3), new DateTime(2013, 4, 9).PreviousDay(DayOfWeek.Wednesday));

            Assert.AreEqual(new DateTime(2013, 4, 7), new DateTime(2013, 4, 14).PreviousDay(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 4, 14), new DateTime(2013, 4, 15).PreviousDay(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 4, 7), new DateTime(2013, 4, 13).PreviousDay(DayOfWeek.Sunday));

            Assert.AreEqual(new DateTime(2013, 4, 13), new DateTime(2013, 4, 20).PreviousDay(DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2013, 4, 20), new DateTime(2013, 4, 21).PreviousDay(DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2013, 4, 13), new DateTime(2013, 4, 19).PreviousDay(DayOfWeek.Saturday));
        }
    }
}
