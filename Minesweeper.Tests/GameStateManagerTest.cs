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
            Assert.AreEqual(true, gameState.IsGameOn);
        }

        [TestMethod]
        public void IsGameOverDefaultValueTest()
        {
            Assert.AreEqual(false, gameState.IsGameOver);
        }

        [TestMethod]
        public void IsNewGameDefaultValueTest()
        {
            Assert.AreEqual(true, gameState.IsNewGame);
        }

        [TestMethod]
        public void IsGameOnSetToTrueShouldKeepTrueAsValueTest()
        {
            gameState.IsGameOver = true;
            Assert.AreEqual(true, gameState.IsGameOn);
        }

        [TestMethod]
        public void IsGameOverSetToTrueShouldKeepTrueAsValueTest()
        {
            gameState.IsGameOver = true;
            Assert.AreEqual(true, gameState.IsGameOver);
        }

        [TestMethod]
        public void IsNewGameSetToTrueShouldKeepTrueAsValueTest()
        {
            gameState.IsGameOver = true;
            Assert.AreEqual(true, gameState.IsNewGame);
        }

        [TestMethod]
        public void IsGameOnSetToFalseShouldKeepFalseAsValueTest()
        {
            gameState.IsGameOver = false;
            Assert.AreEqual(false, gameState.IsGameOver);
        }

        [TestMethod]
        public void IsGameOverSetToFalseShouldKeepFalseAsValueTest()
        {
            gameState.IsGameOver = false;
            Assert.AreEqual(false, gameState.IsGameOver);
        }

        [TestMethod]
        public void IsNewGameSetToFalseShouldKeepFalseAsValueTest()
        {
            gameState.IsGameOver = false;
            Assert.AreEqual(false, gameState.IsGameOver);
        }
    }
}
