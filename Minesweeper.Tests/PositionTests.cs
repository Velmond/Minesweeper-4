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
            this.positon = new Position();
        }

        [TestMethod]
        public void NewPositionValueTest()
        {
            char expected = '?';
            char actual = this.positon.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsBombTest()
        {
            bool expected = false;
            bool actual = this.positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewPositionIsHiddenTest()
        {
            bool expected = true;
            bool actual = this.positon.IsHidden;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombANonBombTest()
        {
            this.positon = new Position('?', true, false);
            this.positon.MakeBomb();

            bool expected = true;
            bool actual = this.positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MakeBombABombTest()
        {
            this.positon = new Position('?', true, true);
            this.positon.MakeBomb();

            bool expected = true;
            bool actual = this.positon.IsBomb;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RevealHiddenTest()
        {
            this.positon = new Position('?', true, false);
            this.positon.Reveal('1');

            char expected = '1';
            char actual = this.positon.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RevealNonHiddenTest()
        {
            this.positon = new Position('3', false, false);
            this.positon.Reveal('1');

            var expected = '3';
            var actual = this.positon.Value;
            Assert.AreEqual(expected, actual);
        }
    }
}
