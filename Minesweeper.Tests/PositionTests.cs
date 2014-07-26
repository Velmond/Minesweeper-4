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
            char expected = '?';
            char actual = positon.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsBombTest()
        {
            Position positon = new Position();
            bool expected = false;
            bool actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsHiddenTest()
        {
            Position positon = new Position();
            bool expected = true;
            bool actual = positon.IsHidden;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombANonBombTest()
        {
            Position positon = new Position('?', true, false);
            positon.MakeBomb();

            bool expected = true;
            bool actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombABombTest()
        {
            Position positon = new Position('?', true, true);
            positon.MakeBomb();

            bool expected = true;
            bool actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RevealHiddenTest()
        {
            Position positon = new Position('?', true, false);
            positon.Reveal('1');

            char expected = '1';
            char actual = positon.Value;
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
