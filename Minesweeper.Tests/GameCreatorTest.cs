namespace Minesweeper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Field;
    using Minesweeper.Field.Contracts;
    using Minesweeper.GameFactory;
    using Minesweeper.Rendering;
    using Minesweeper.Scoring;
    using Minesweeper.Scoring.Contracts;

    [TestClass]
    public class GameCreatorTest
    {
        private GameCreator creator;

        [TestInitialize]
        public void Init()
        {
            this.creator = new GameCreator();
        }

        [TestMethod]
        public void CreateScoreBoardTest()
        {
            var received = this.creator.CreateScoreBoard();

            Assert.IsInstanceOfType(received, typeof(ScoreBoard));
        }

        [TestMethod]
        public void CreateGameFieldSaveTest()
        {
            var received = this.creator.CreateGameFieldSave();

            Assert.IsInstanceOfType(received, typeof(IGameFieldSave));
        }

        [TestMethod]
        public void CreateScoreRecordTest()
        {
            string name = "Joe";
            int score = 8;
            var received = this.creator.CreateScoreRecord(name, score);

            Assert.IsInstanceOfType(received, typeof(IScoreRecord));
        }

        [TestMethod]
        public void CreateScoreRecordNameTest()
        {
            string name = "Joe";
            int score = 8;
            IScoreRecord received = this.creator.CreateScoreRecord(name, score);

            Assert.AreEqual(name, received.PlayerName);
        }

        [TestMethod]
        public void CreateScoreRecordScoreTest()
        {
            string name = "Joe";
            int score = 8;
            IScoreRecord received = this.creator.CreateScoreRecord(name, score);

            Assert.AreEqual(score, received.PlayerScore);
        }

        [TestMethod]
        public void CreateRendererTest()
        {
            var received = this.creator.CreateRenderer(new ScoreBoard(), new GameField());

            Assert.IsInstanceOfType(received, typeof(ConsoleRenderer));
        }

        [TestMethod]
        public void CreateGameFieldTest()
        {
            var received = this.creator.CreateGameField();

            Assert.IsInstanceOfType(received, typeof(GameField));
        }
    }
}
