namespace Minesweeper.Tests
{
    using Minesweeper.Rendering;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Scoring;

    [TestClass]
    public class RendererTest
    {
        [TestMethod]
        public void RendererConstructorTest()
        {
            GameField field = new GameField();
            ScoreBoard scores = new ScoreBoard();
            Renderer renderer = new Renderer(scores, field);
            bool areEqual = renderer.GameField == field && renderer.ScoreBoard == scores;
            Assert.IsTrue(areEqual);
        }
    }
}
