using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rebuild.Extensions
{
    public static class ObjectExtensions
    {
        public static T AssertEqual<T>(this T actual, T expected)
        {
            Assert.AreEqual(expected, actual);
            return actual;
        }
    }
}
