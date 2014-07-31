namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Field;
    using Minesweeper.Field.Contracts;
    using Minesweeper.Rendering;
    using Minesweeper.Rendering.Contracts;
    using Minesweeper.Scoring;
    using Minesweeper.Scoring.Contracts;

    [TestClass]
    public class RendererTest
    {
        private IGameField field;
        private IScoreBoard scores;
        private IRenderer renderer;

        [TestInitialize]
        public void Init()
        {
            this.field = new GameField();
            this.scores = new ScoreBoard();
            this.renderer = new ConsoleRenderer(this.scores, this.field);
        }

        [TestMethod]
        public void RendererConstructorTest()
        {
            bool areEqual = this.renderer.GameField == this.field && this.renderer.ScoreBoard == this.scores;
            Assert.IsTrue(areEqual);
        }
    }
}
