using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Rebuild.Extensions
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void BinarySearch()
        {
            var items = new[] { 1, 3, 4, 5 };
            items.BinarySearch(3).AssertEqual(1);
            (~items.BinarySearch(2)).AssertEqual(1);
        }

        [TestMethod]
        public void BinaryInsert()
        {
            var array = new [] { 1, 3, 4 };
            array
                .ToList()
                .BinaryInsert(2)
                .AssertSequenceEquals(new [] { 1, 2, 3, 4 });

            array
                .ToList()
                .BinaryInsert(3, insertStrategy: BinaryInsertStrategy.DoNothingIfFound)
                .AssertSequenceEquals(array);

            array
                .ToList()
                .BinaryInsert(3, insertStrategy: BinaryInsertStrategy.Replace)
                .AssertSequenceEquals(array);

            array
                .ToList()
                .BinaryInsert(4, insertStrategy: BinaryInsertStrategy.InsertAfter)
                .AssertSequenceEquals(new [] { 1, 3, 4, 4});

            array
                .ToList()
                .BinaryInsert(1, insertStrategy: BinaryInsertStrategy.InsertBefore)
                .AssertSequenceEquals(new[] { 1, 1, 3, 4 });

        }

        [TestMethod]
        public void BinaryRemove()
        {
            var array = new[] { 1, 3, 4 };

            array
                .ToList()
                .Do(list => list.BinaryRemove(3).AssertEqual(true))
                .AssertSequenceEquals(new[] { 1, 4 });

            array
                .ToList()
                .BinaryRemove(2)
                .AssertEqual(false);

        }
    }
}
