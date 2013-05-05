using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rebuild.Extensions
{
    public static class ObjectExtensions
    {
        public static T AssertEqual<T>(this T actual, T expected)
        {
            var n = new int[0];


            Assert.AreEqual(expected, actual);
            return actual;
        }
    }
}
