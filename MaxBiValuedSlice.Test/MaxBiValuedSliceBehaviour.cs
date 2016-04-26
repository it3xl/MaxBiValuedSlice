using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaxBiValuedSlice.Test
{
    [TestClass]
    public class MaxBiValuedSliceBehaviour
    {
        [DebuggerStepThrough]
        private int _getResult(int[] ints)
        {
            return new Solution()
                .solution(ints);
        }

        [TestMethod]
        public void FromTask54450And12()
        {
            Assert.AreEqual(4, _getResult(new[] { 5, 4, 4, 5, 0, 12 }));
        }

        [TestMethod]
        public void BoundaryValues()
        {
            Assert.AreEqual(6, _getResult(new[] { int.MaxValue, int.MinValue, int.MaxValue, int.MaxValue, int.MinValue, int.MinValue }));
        }

        [TestMethod]
        public void Empty()
        {
            Assert.AreEqual(0, _getResult(new int[0]));
        }

        [TestMethod]
        public void OneItem()
        {
            Assert.AreEqual(1, _getResult(new[] { -12 }));
        }

        [TestMethod]
        public void TwoEqualItems()
        {
            Assert.AreEqual(2, _getResult(new[] { 7, 7 }));
        }

        [TestMethod]
        public void TwoNotEqualItems()
        {
            Assert.AreEqual(2, _getResult(new[] { 177, -64 }));
        }

        [TestMethod]
        public void Overlapping1331()
        {
            Assert.AreEqual(4, _getResult(new[] { 2, 1, 3, 3, 1, 4, 5, 6 }));
        }

        [TestMethod]
        public void Items21332456()
        {
            Assert.AreEqual(3, _getResult(new[] { 2, 1, 3, 3, 2, 4, 5, 6 }));
        }

        [TestMethod]
        public void Items33344()
        {
            Assert.AreEqual(5, _getResult(new[] { 3, 3, 3, 4, 4 }));
        }

        [TestMethod]
        public void Items333447()
        {
            Assert.AreEqual(5, _getResult(new[] { 3, 3, 3, 4, 4, 7 }));
        }

        [TestMethod]
        public void Items1333447()
        {
            Assert.AreEqual(5, _getResult(new[] { 1, 3, 3, 3, 4, 4, 7 }));
        }

        [TestMethod]
        public void Items1777447()
        {
            Assert.AreEqual(6, _getResult(new[] { 1, 7, 7, 7, 4, 4, 7 }));
        }

        [TestMethod]
        public void Items1333443447()
        {
            Assert.AreEqual(8, _getResult(new[] { 1, 3, 3, 3, 4, 4, 3, 4, 4, 7 }));
        }

        [TestMethod]
        public void LastBiggest()
        {
            Assert.AreEqual(5, _getResult(new[] { 1, 3, 3, 2, 2, 7, 7, 7 }));
        }

        [TestMethod]
        public void MiddleBiggest()
        {
            Assert.AreEqual(5, _getResult(new[] { 1, 3, 3, 2, 2, 7, 7, 7, 6 }));
        }

        [TestMethod]
        public void LastSingle()
        {
            Assert.AreEqual(4, _getResult(new[] { 1, 3, 3, 2, 7, 7, 7, 6 }));
        }

        [TestMethod]
        public void Items133227772()
        {
            Assert.AreEqual(6, _getResult(new[] { 1, 3, 3, 2, 2, 7, 7, 7, 2 }));
        }

        [TestMethod]
        public void Items12322444()
        {
            Assert.AreEqual(5, _getResult(new[] { 1, 2, 3, 2, 2, 4, 4, 4 }));
        }

        [TestMethod]
        public void Items2322444()
        {
            Assert.AreEqual(5, _getResult(new[] { 2, 3, 2, 2, 4, 4, 4 }));
        }

        [TestMethod]
        public void Items23224442()
        {
            Assert.AreEqual(6, _getResult(new[] { 2, 3, 2, 2, 4, 4, 4, 2 }));
        }

        [TestMethod]
        public void Items2322444222()
        {
            Assert.AreEqual(8, _getResult(new[] { 2, 3, 2, 2, 4, 4, 4, 2, 2, 2 }));
        }

        [TestMethod]
        public void Items232244445()
        {
            Assert.AreEqual(6, _getResult(new[] { 2, 3, 2, 2, 4, 4, 4, 4, 5 }));
        }

        [TestMethod]
        public void Items232242424222()
        {
            Assert.AreEqual(10, _getResult(new[] { 2, 3, 2, 2, 4, 2, 4, 2, 4, 2, 2, 2 }));
        }




    }
}
