namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void NewPositionValueTest()
        {
            Position positon = new Position();
            var expected = '?';
            var actual = positon.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsBombTest()
        {
            Position positon = new Position();
            var expected = false;
            var actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsHiddenTest()
        {
            Position positon = new Position();
            var expected = true;
            var actual = positon.IsHidden;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombANonBombTest()
        {
            Position positon = new Position('?', true, false);
            positon.MakeBomb();

            var expected = true;
            var actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombABombTest()
        {
            Position positon = new Position('?', true, true);
            positon.MakeBomb();

            var expected = true;
            var actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RevealHiddenTest()
        {
            Position positon = new Position('?', true, false);
            positon.Reveal('1');

            var expected = '1';
            var actual = positon.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RevealNonHiddenTest()
        {
            Position positon = new Position('3', false, false);
            positon.Reveal('1');

            var expected = '3';
            var actual = positon.Value;
            Assert.AreEqual(expected, actual);
        }
    }
}
