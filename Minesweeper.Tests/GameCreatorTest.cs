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
        [TestMethod]
        public void CreateScoreBoardTest()
        {
            GameCreator creator = new GameCreator();
            var received = creator.CreateScoreBoard();

            Assert.IsInstanceOfType(received, typeof(ScoreBoard));
        }

        [TestMethod]
        public void CreateGameFieldSaveTest()
        {
            GameCreator creator = new GameCreator();
            var received = creator.CreateGameFieldSave();

            Assert.IsInstanceOfType(received, typeof(IGameFieldSave));
        }

        [TestMethod]
        public void CreateScoreRecordTest()
        {
            GameCreator creator = new GameCreator();
            string name = "Joe";
            int score = 8;
            var received = creator.CreateScoreRecord(name, score);

            Assert.IsInstanceOfType(received, typeof(IScoreRecord));
        }

        [TestMethod]
        public void CreateScoreRecordNameTest()
        {
            GameCreator creator = new GameCreator();
            string name = "Joe";
            int score = 8;
            IScoreRecord received = creator.CreateScoreRecord(name, score);

            Assert.AreEqual(name, received.PlayerName);
        }

        [TestMethod]
        public void CreateScoreRecordScoreTest()
        {
            GameCreator creator = new GameCreator();
            string name = "Joe";
            int score = 8;
            IScoreRecord received = creator.CreateScoreRecord(name, score);

            Assert.AreEqual(score, received.PlayerScore);
        }

        [TestMethod]
        public void CreateRendererTest()
        {
            GameCreator creator = new GameCreator();
            var received = creator.CreateRenderer(new ScoreBoard(), new GameField());

            Assert.IsInstanceOfType(received, typeof(Renderer));
        }

        [TestMethod]
        public void CreateGameFieldTest()
        {
            GameCreator creator = new GameCreator();
            var received = creator.CreateGameField();

            Assert.IsInstanceOfType(received, typeof(GameField));
        }
    }
}
