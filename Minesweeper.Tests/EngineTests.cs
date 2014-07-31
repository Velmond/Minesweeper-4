namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EngineTests
    {
        private Engine engine;

        [TestInitialize]
        public void Inin()
        {
            this.engine = Engine.Instance;
        }

        [TestMethod]
        public void DefaultValueSetForIsGameWonTest()
        {
            Assert.AreEqual(false, this.engine.IsGameWon);
        }

        [TestMethod]
        public void DefaultValueSetForCurrentScoreTest()
        {
            Assert.AreEqual(0, this.engine.CurrentScore);
        }
    }
}
