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
        [TestMethod]
        public void RendererConstructorTest()
        {
            IGameField field = new GameField();
            IScoreBoard scores = new ScoreBoard();
            Renderer renderer = new Renderer(scores, field);
            bool areEqual = renderer.GameField == field && renderer.ScoreBoard == scores;
            Assert.IsTrue(areEqual);
        }
    }
}
