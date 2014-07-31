namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Minesweeper.Field;
    using Minesweeper.Field.Contracts;
    using Minesweeper.Rendering;
    using Minesweeper.Scoring;
    using Minesweeper.Scoring.Contracts;

    [TestClass]
    public class RendererTest
    {
        private IGameField field;
        private IScoreBoard scores;
        private Renderer renderer;

        [TestInitialize]
        public void Init()
        {
            field = new GameField();
            scores = new ScoreBoard();
            renderer = new Renderer(scores, field);
        }

        [TestMethod]
        public void RendererConstructorTest()
        {
            bool areEqual = renderer.GameField == field && renderer.ScoreBoard == scores;
            Assert.IsTrue(areEqual);
        }
    }
}
