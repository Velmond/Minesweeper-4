namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameStateManagerTest
    {
        private GameStateManager gameState;

        [TestInitialize]
        public void Init()
        {
            this.gameState = GameStateManager.Instance;
        }
        
        [TestMethod]
        public void IsGameOnDefaultValueTest()
        {
            Assert.AreEqual(true, this.gameState.IsGameOn);
        }

        [TestMethod]
        public void IsGameOverDefaultValueTest()
        {
            Assert.AreEqual(false, this.gameState.IsGameOver);
        }

        [TestMethod]
        public void IsNewGameDefaultValueTest()
        {
            Assert.AreEqual(true, this.gameState.IsNewGame);
        }

        [TestMethod]
        public void IsGameOnSetToTrueShouldKeepTrueAsValueTest()
        {
            this.gameState.IsGameOver = true;
            Assert.AreEqual(true, this.gameState.IsGameOn);
        }

        [TestMethod]
        public void IsGameOverSetToTrueShouldKeepTrueAsValueTest()
        {
            this.gameState.IsGameOver = true;
            Assert.AreEqual(true, this.gameState.IsGameOver);
        }

        [TestMethod]
        public void IsNewGameSetToTrueShouldKeepTrueAsValueTest()
        {
            this.gameState.IsGameOver = true;
            Assert.AreEqual(true, this.gameState.IsNewGame);
        }

        [TestMethod]
        public void IsGameOnSetToFalseShouldKeepFalseAsValueTest()
        {
            this.gameState.IsGameOver = false;
            Assert.AreEqual(false, this.gameState.IsGameOver);
        }

        [TestMethod]
        public void IsGameOverSetToFalseShouldKeepFalseAsValueTest()
        {
            this.gameState.IsGameOver = false;
            Assert.AreEqual(false, this.gameState.IsGameOver);
        }

        [TestMethod]
        public void IsNewGameSetToFalseShouldKeepFalseAsValueTest()
        {
            this.gameState.IsGameOver = false;
            Assert.AreEqual(false, this.gameState.IsGameOver);
        }
    }
}
