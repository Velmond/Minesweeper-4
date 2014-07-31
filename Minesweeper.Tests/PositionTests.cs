namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Field;

    [TestClass]
    public class PositionTests
    {
        private Position positon;

        [TestInitialize]
        public void Init()
        {
            positon = new Position();
        }

        
        [TestMethod]
        public void NewPositionValueTest()
        {
            char expected = '?';
            char actual = positon.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsBombTest()
        {
            bool expected = false;
            bool actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsHiddenTest()
        {
            bool expected = true;
            bool actual = positon.IsHidden;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombANonBombTest()
        {
            positon = new Position('?', true, false);
            positon.MakeBomb();

            bool expected = true;
            bool actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombABombTest()
        {
            positon = new Position('?', true, true);
            positon.MakeBomb();

            bool expected = true;
            bool actual = positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RevealHiddenTest()
        {
            positon = new Position('?', true, false);
            positon.Reveal('1');

            char expected = '1';
            char actual = positon.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RevealNonHiddenTest()
        {
            positon = new Position('3', false, false);
            positon.Reveal('1');

            var expected = '3';
            var actual = positon.Value;
            Assert.AreEqual(expected, actual);
        }
    }
}
