namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Controls;
    using Minesweeper.Field;
    using Minesweeper.Field.Contracts;
    using Minesweeper.GameFactory;
    using Minesweeper.Rendering;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Scoring;
    using Minesweeper.Scoring.Contracts;

    [TestClass]
    public class ControlManagerTests
    {
        private ControlManager controlManager;
        private IScoreBoard scoreBoard;
        private Creator gameCreator;
        private IGameFieldSave gameFieldSave;
        private IRenderer renderer;
        private GameStateManager gameState;

        [TestInitialize]
        public void Inin()
        {
            this.scoreBoard = new ScoreBoard();
            this.gameCreator = new GameCreator();
            this.gameFieldSave = new GameFieldSave();
            this.renderer = new ConsoleRenderer(this.scoreBoard, new GameField());
            this.gameState = GameStateManager.Instance;
            this.controlManager = new ControlManager(this.renderer, this.scoreBoard, this.gameCreator, this.gameFieldSave, this.gameState);
        }

        [TestMethod]
        public void ScoreBoardShouldReturnScoreBoardThatWasGivenToItTest()
        {
            Assert.AreEqual(this.scoreBoard, this.controlManager.ScoreBoard);
        }

        [TestMethod]
        public void GameCreatorShouldReturnGameCreatorThatWasGivenToItTest()
        {
            Assert.AreEqual(this.gameCreator, this.controlManager.Creator);
        }

        [TestMethod]
        public void GameFieldSaveShouldReturnGameFieldSaveThatWasGivenToItTest()
        {
            Assert.AreEqual(this.gameFieldSave, this.controlManager.GameFieldSave);
        }

        [TestMethod]
        public void RendererSaveShouldReturnRendererSaveThatWasGivenToItTest()
        {
            Assert.AreEqual(this.renderer, this.controlManager.Renderer);
        }

        [TestMethod]
        public void GameStateSaveShouldReturnGameStateSaveThatWasGivenToItTest()
        {
            Assert.AreEqual(this.gameState, this.controlManager.GameState);
        }

        [TestMethod]
        public void ExitApplicationCommandShouldSetIsGameOnToFalseTest()
        {
            this.controlManager.ExitApplicationCommand();
            Assert.AreEqual(false, this.controlManager.GameState.IsGameOn);
        }

        [TestMethod]
        public void RestartApplicationCommandSholdSetIsGameOverToFalseTest()
        {
            this.controlManager.RestartApplicationCommand();
            Assert.AreEqual(false, this.controlManager.GameState.IsGameOver);
        }

        [TestMethod]
        public void RestartApplicationCommandSholdSetIsNewGameToTrueTest()
        {
            this.controlManager.RestartApplicationCommand();
            Assert.AreEqual(true, this.controlManager.GameState.IsNewGame);
        }
    }
}
