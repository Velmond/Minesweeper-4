namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Controls;
    using Minesweeper.Rendering;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Field;
    using Minesweeper.Scoring;
    using Minesweeper.Scoring.Contracts;
    using Minesweeper.GameFactory;
    using Minesweeper.Field.Contracts;

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
            scoreBoard = new ScoreBoard();
            gameCreator = new GameCreator();
            gameFieldSave = new GameFieldSave();
            renderer = new Renderer(scoreBoard, new GameField());
            gameState = GameStateManager.Instance;
            controlManager = new ControlManager(renderer, scoreBoard, gameCreator, gameFieldSave, gameState);
        }

        [TestMethod]
        public void ScoreBoardShouldReturnScoreBoardThatWasGivenToItTest()
        {
            Assert.AreEqual(scoreBoard, controlManager.ScoreBoard);
        }

        [TestMethod]
        public void GameCreatorShouldReturnGameCreatorThatWasGivenToItTest()
        {
            Assert.AreEqual(gameCreator, controlManager.Creator);
        }

        [TestMethod]
        public void GameFieldSaveShouldReturnGameFieldSaveThatWasGivenToItTest()
        {
            Assert.AreEqual(gameFieldSave, controlManager.GameFieldSave);
        }

        [TestMethod]
        public void RendererSaveShouldReturnRendererSaveThatWasGivenToItTest()
        {
            Assert.AreEqual(renderer, controlManager.Renderer);
        }

        [TestMethod]
        public void GameStateSaveShouldReturnGameStateSaveThatWasGivenToItTest()
        {
            Assert.AreEqual(gameState, controlManager.GameState);
        }

        [TestMethod]
        public void ExitApplicationCommandShouldSetIsGameOnToFalseTest()
        {
            controlManager.ExitApplicationCommand();
            Assert.AreEqual(false, controlManager.GameState.IsGameOn);
        }

        [TestMethod]
        public void RestartApplicationCommandSholdSetIsGameOverToFalseTest()
        {
            controlManager.RestartApplicationCommand();
            Assert.AreEqual(false, controlManager.GameState.IsGameOver);
        }

        [TestMethod]
        public void RestartApplicationCommandSholdSetIsNewGameToTrueTest()
        {
            controlManager.RestartApplicationCommand();
            Assert.AreEqual(true, controlManager.GameState.IsNewGame);
        }
    }
}
